using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CallCenter.Client.Enums;
using CallCenter.Client.Models;
using CallCenter.Client.Services.Interfaces.Services;
using CallCenter.Client.ViewModels.Helpers.Interfaces;
using CallCenter.Client.ViewModels.ViewModels.Base;

namespace CallCenter.Client.ViewModels.ViewModels.Conversation
{
    public class PanelViewModel : BaseViewModel
    {
        private int _employeeId;
        private int _lastMessageId;
        private int _conversationId;

        public EmployeeStatus SelectedStatus { get; set; }
        private readonly IEmployeeService _employeeService;
        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;

        public BindableCollection<MessageModel> Messages { get; set; }

        public IList<EmployeeStatus> Statuses => Enum.GetValues(typeof(EmployeeStatus)).Cast<EmployeeStatus>().ToList();

        public PanelViewModel(IContainerManager containerManager, IEmployeeService employeeService, IConversationService conversationService, IMessageService messageService) : base(containerManager)
        {
            _employeeService = employeeService;
            _conversationService = conversationService;
            _messageService = messageService;
            SelectedStatus = EmployeeStatus.Offline;
        }

        protected override async void OnActivate()
        {
            base.OnActivate();

            _employeeId = await _employeeService.GetEmployeeId();
        }

        public async void ChangeStatus()
        {
            await _employeeService.UpdateStatus(_employeeId, SelectedStatus);
        }

        public async void FindConversation()
        {
            var conversation = await _conversationService.FindConversationForEmployee(_employeeId);

            if (conversation == null)
                return;

            _conversationId = conversation.Id;

            var messages = await GetMessages();

            if (messages.Count == 0)
                _lastMessageId = 0;

            _lastMessageId = messages.Last().Id;
        }

        public async void GetNewMessages()
        {
            var messages = await GetMessages();

            if (messages.Count == 0)
                return;

            var newMessages = messages.Where(m => m.Id > _lastMessageId).ToList();

            Messages.AddRange(newMessages);
        }

        private async Task<IList<MessageModel>> GetMessages()
        {
            var messages = await _messageService.GetAllMessagesForConversation(_conversationId);

            return messages;
        }
    }
}
