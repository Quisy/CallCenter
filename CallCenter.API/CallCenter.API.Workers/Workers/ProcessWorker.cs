﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using CallCenter.API.Enums;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Models.Conversation;
using CallCenter.API.Services.Interfaces.Services.Facebook;
using CallCenter.API.Services.Interfaces.Services.Membership;
using CallCenter.API.Utils.Helpers.Interfaces;
using CallCenter.API.Workers.Interfaces.Workers;

namespace CallCenter.API.Workers.Workers
{
    public class ProcessWorker : IProcessWorker
    {
        private readonly ISettingsManager _settingsManager;
        private readonly Services.Interfaces.Services.Facebook.IConversationService _facebookConversationService;
        private readonly Services.Interfaces.Services.Facebook.IMessageService _facebbokMessageService;
        private readonly Services.Interfaces.Services.Conversation.IConversationService _conversationService;
        private readonly Services.Interfaces.Services.Conversation.IMessageService _messageService;
        private readonly IActivitiWorker _activitiWorker;
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;

        public ProcessWorker(Services.Interfaces.Services.Facebook.IConversationService facebookConversationService, Services.Interfaces.Services.Conversation.IConversationService conversationService, ISettingsManager settingsManager, IMessageService messageService, IActivitiWorker activitiWorker, Services.Interfaces.Services.Conversation.IMessageService messageService1, ICustomerService customerService, IEmployeeService employeeService)
        {
            _facebookConversationService = facebookConversationService;
            _conversationService = conversationService;
            _settingsManager = settingsManager;
            _facebbokMessageService = messageService;
            _activitiWorker = activitiWorker;
            _messageService = messageService1;
            _customerService = customerService;
            _employeeService = employeeService;
        }

        public async Task GetFacebookConversationsAndManage()
        {
            var facebookConversationsResult =
                await _facebookConversationService.GetPageConversationsWithUnreadMessages(
                    _settingsManager.Load(Constants.SettingsConstants.FacebookPageIdKeyName));

            if (facebookConversationsResult.IsError)
                return;

            var facebookConversations = facebookConversationsResult.Value;

            foreach (var fbConversation in facebookConversations)
            {
                var conversationResult = _conversationService.GetConversationByFacebookCnversationId(fbConversation.Id);

                // If new conversation
                if (conversationResult.IsWarning)
                {
                    var processInstanceResult = await _activitiWorker.StartInstanceOfProcessDefinitionAsync(_settingsManager.Load(Constants
                        .SettingsConstants.ActivitiTalkProcessNameKeyName));

                    if (processInstanceResult.IsError)
                        continue;

                    var processInstance = processInstanceResult.Value;

                    var conversationAddResult =
                        _conversationService.AddNewConversation(fbConversation.Id, processInstance.Id);

                    if (conversationAddResult.IsError)
                        continue;

                    var addedConversation = conversationAddResult.Value;

                    await this.SendMessage(addedConversation, "1-GetData, 2-Coversation");

                    continue;
                }

                var conversation = conversationResult.Value;

                var fbMessagesResult = await _facebbokMessageService.GetConversationMessages(conversation.FacebookConversationId);
                var messagesResult = _messageService.GetMessagesToForConversation(conversation.Id);

                if (fbMessagesResult.IsError || messagesResult.IsError)
                    continue;

                var conversationFbMessages = fbMessagesResult.Value;
                var conversationMessages = messagesResult.Value;

                var lastFbMessage = conversationFbMessages.SingleOrDefault(fbm => fbm.Id.Equals(conversationMessages.Last()
                    .FacebookMessageId));

                if (lastFbMessage == null)
                    continue;

                conversationFbMessages = conversationFbMessages.OrderBy(m => m.CreatedTime).ToList();

                if (conversationFbMessages.IndexOf(lastFbMessage) == conversationFbMessages.Count - 1)
                    continue;

                if (conversationFbMessages.Skip(conversationFbMessages.IndexOf(lastFbMessage)).Any(fbm => !conversationMessages.Select(m => m.FacebookMessageId)
                    .Contains(fbm.Id)))
                {
                    if (conversation.ProcessInstanceId.HasValue)
                    {
                        var taskResult =
                            await _activitiWorker.GetCurrentTaskForInstance((int)conversation.ProcessInstanceId);
                        if (taskResult.IsError)
                            continue;

                        await ManageProcess(conversation, taskResult.Value);
                    }
                    else
                    {
                        var processInstanceResult = await _activitiWorker.StartInstanceOfProcessDefinitionAsync(_settingsManager.Load(Constants
                            .SettingsConstants.ActivitiTalkProcessNameKeyName));

                        if (processInstanceResult.IsError)
                            continue;

                        var processInstance = processInstanceResult.Value;

                        conversation.ProcessInstanceId = processInstance.Id;
                        conversation.ProcessTask = TalkProcessTask.Manager;
                        conversation.Messages = null;

                        _conversationService.Update(conversation);

                        await this.SendMessage(conversation, "1-GetData, 2-Coversation");
                    }
                }

            }
        }

        private async Task ManageProcess(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            switch (task.ProcessTask)
            {
                case TalkProcessTask.Manager:
                    await Manager(conversation, task);
                    break;

                case TalkProcessTask.None:
                    break;
                case TalkProcessTask.CheckPass:
                    await CheckPass(conversation, task);
                    break;
                case TalkProcessTask.ReturnData:
                    break;
                case TalkProcessTask.CheckAdviser:
                    await CheckAdviser(conversation, task);
                    break;
                case TalkProcessTask.AttachToAdviser:
                    await AttachToAdviser(conversation, task);
                    break;
                case TalkProcessTask.Conversation:
                    await Conversation(conversation, task);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task ChangeTask(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            switch (task.ProcessTask)
            {
                case TalkProcessTask.Manager:
                    break;
                case TalkProcessTask.None:
                    break;
                case TalkProcessTask.CheckPass:
                    await this.SendMessage(conversation, "Pass login and password (login/password)");
                    break;
                case TalkProcessTask.ReturnData:
                    await ReturnData(conversation, task);
                    break;
                case TalkProcessTask.CheckAdviser:
                    break;
                case TalkProcessTask.AttachToAdviser:
                    break;
                case TalkProcessTask.Conversation:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task Manager(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            var messagesResult = await _facebbokMessageService.GetConversationMessages(conversation.FacebookConversationId);

            var messages = messagesResult.Value;
          

            string message = messages.FirstOrDefault()?.Message;

            if (message == null)
                return;

            TaskFormModel form = new TaskFormModel();

            if (message.Trim().Equals("1"))
            {
                form = new TaskFormModel
                {
                    TaskId = task.Id,
                    Properties = new List<TaskFormProperty>
                    {
                        new TaskFormProperty
                        {
                            Id = "status",
                            Value = "logowanie"
                        }
                    }
                };
            }
            else if (message.Trim().Equals("2"))
            {
                form = new TaskFormModel
                {
                    TaskId = task.Id,
                    Properties = new List<TaskFormProperty>
                    {
                        new TaskFormProperty
                        {
                            Id = "status",
                            Value = "rozmowa"
                        }
                    }
                };
            }

            var nextTaskResult = await _activitiWorker.CompleteTaskSubmittingFormAndGetNextAsync((int)conversation.ProcessInstanceId, form);
            var nextTask = nextTaskResult.Value;
            conversation.ProcessTask = nextTask.ProcessTask;
            conversation.Messages = null;
            _conversationService.Update(conversation);
            await ChangeTask(conversation, nextTask);
        }

        private async Task CheckPass(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            var messagesResult = await _facebbokMessageService.GetConversationMessages(conversation.FacebookConversationId);

            var messages = messagesResult.Value;

            string message = messages.FirstOrDefault()?.Message;

            if (message == null)
                return;

            string[] credentials = message.Split('/');

            if (credentials.Length != 2)
                return;

            string login = credentials[0];
            string password = credentials[1];

            var customerResult = await _customerService.GetCustomerByCredentials(login, password);

            TaskFormModel form;

            if (customerResult.IsError)
            {
                form = new TaskFormModel
                {
                    TaskId = task.Id,
                    Properties = new List<TaskFormProperty>
                    {
                        new TaskFormProperty
                        {
                            Id = "isOk",
                            Value = "false"
                        }
                    }
                };
            }
            else
            {
                form = new TaskFormModel
                {
                    TaskId = task.Id,
                    Properties = new List<TaskFormProperty>
                    {
                        new TaskFormProperty
                        {
                            Id = "isOk",
                            Value = "true"
                        }
                    }
                };

                conversation.CustomerId = customerResult.Value.Id;
            }

            var nextTaskResult = await _activitiWorker.CompleteTaskSubmittingFormAndGetNextAsync((int)conversation.ProcessInstanceId, form);
            var nextTask = nextTaskResult.Value;
            conversation.ProcessTask = nextTask.ProcessTask;
            conversation.Messages = null;
            _conversationService.Update(conversation);
            await ChangeTask(conversation, nextTask);
        }

        private async Task ReturnData(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            var cutomerResult = _customerService.GetById((int)conversation.CustomerId);

            if (cutomerResult.IsError)
                return;

            await this.SendMessage(conversation, $"Account balance: {cutomerResult.Value.AccountBalance}");

            await _activitiWorker.CompleteTaskAndGetNextAsync((int)conversation.ProcessInstanceId, task.Id);

            conversation.CustomerId = null;
            conversation.ProcessInstanceId = null;
            conversation.ProcessTask = TalkProcessTask.None;
            _conversationService.Update(conversation);
        }

        private async Task CheckAdviser(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            var employeeResult = _employeeService.GetAvailableEmployee();
            TaskFormModel form;


            if (employeeResult.IsWarning || employeeResult.IsError)
            {
                await this.SendMessage(conversation, "No available consultant found, try again later.");

                form = new TaskFormModel
                {
                    TaskId = task.Id,
                    Properties = new List<TaskFormProperty>
                    {
                        new TaskFormProperty
                        {
                            Id = "isAvailable",
                            Value = "false"
                        }
                    }
                };

                await _activitiWorker.CompleteTaskSubmittingFormAndGetNextAsync((int)conversation.ProcessInstanceId, form);

                conversation.CustomerId = null;
                conversation.ProcessInstanceId = null;
                conversation.ProcessTask = TalkProcessTask.None;
                _conversationService.Update(conversation);
            }
            else
            {
                form = new TaskFormModel
                {
                    TaskId = task.Id,
                    Properties = new List<TaskFormProperty>
                    {
                        new TaskFormProperty
                        {
                            Id = "isAvailable",
                            Value = "true"
                        }
                    }
                };

                var nextTaskResult = await _activitiWorker.CompleteTaskSubmittingFormAndGetNextAsync((int)conversation.ProcessInstanceId, form);
                var nextTask = nextTaskResult.Value;
                conversation.ProcessTask = nextTask.ProcessTask;
                conversation.Messages = null;
                _conversationService.Update(conversation);
                await ChangeTask(conversation, nextTask);
            }

           
        }

        private async Task AttachToAdviser(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            var employeeResult = _employeeService.GetAvailableEmployee();

            var employee = employeeResult.Value;

            var nextTaskResult = await _activitiWorker.CompleteTaskAndGetNextAsync((int)conversation.ProcessInstanceId, task.Id);
            var nextTask = nextTaskResult.Value;
            conversation.ProcessTask = nextTask.ProcessTask;
            conversation.AssignedEmployeeId = employee.Id;
            conversation.Messages = null;
            _conversationService.Update(conversation);

            employee.Status = EmployeeStatus.Busy;
            _employeeService.Update(employee);

            await this.SendMessage(conversation, "Wait for message from consultant");

            await ChangeTask(conversation, nextTask);

        }

        private async Task Conversation(Models.Conversation.ConversationModel conversation, TaskModel task)
        {
            var fbMessagesResult = await _facebbokMessageService.GetConversationMessages(conversation.FacebookConversationId);
            var messagesResult = _messageService.GetMessagesToForConversation(conversation.Id);

            if (fbMessagesResult.IsError || messagesResult.IsError)
                return;

            var conversationFbMessages = fbMessagesResult.Value;
            var conversationMessages = messagesResult.Value;

            var lastFbMessage = conversationFbMessages.SingleOrDefault(fbm => fbm.Id.Equals(conversationMessages.Last()
                .FacebookMessageId));

            if (lastFbMessage == null)
                return;

            conversationFbMessages = conversationFbMessages.OrderBy(m => m.CreatedTime).ToList();

            var newMessages = conversationFbMessages.Skip(conversationFbMessages.IndexOf(lastFbMessage) + 1).ToList();

            foreach (var messaage in newMessages)
            {
                _messageService.AddMessageToConversation(conversation.Id,
                    messaage.Message, messaage.Id);
            }
        }

        private async Task SendMessage(Models.Conversation.ConversationModel conversation, string message)
        {
            var fbMesageResult = await _facebbokMessageService.SendMessageToConversation(conversation.FacebookConversationId, message);

            if (fbMesageResult.IsError)
                return;

            var fbMessage = fbMesageResult.Value;

            _messageService.AddMessageToConversation(conversation.Id,
                message, fbMessage.Id);
        }

    }
}
