using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CallCenter.API.Enums;
using CallCenter.API.Models.Conversation;
using CallCenter.API.Services.Interfaces.Services.Conversation;
using CallCenter.API.ViewModels.Conversation;
using CallCenter.API.Web.Controllers.Base;
using CallCenter.API.Workers.Interfaces.Workers;
using Microsoft.AspNet.Identity;

namespace CallCenter.API.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/conversation")]
    public class ConversationController : BaseController
    {
        private readonly IConversationService _conversationService;
        private readonly IActivitiWorker _activitiWorker;

        public ConversationController(IConversationService conversationService, IActivitiWorker activitiWorker)
        {
            _conversationService = conversationService;
            _activitiWorker = activitiWorker;
        }

        [HttpGet]
        public IHttpActionResult GetConversationForUser(int employeeId)
        {
            var conversationResult = _conversationService.GetConversationAssignedToEmployee(employeeId);

            if (conversationResult.IsError)
                return NotFound();

            var conversationModel = conversationResult.Value;

            var conversation = new ConversationGetViewModel
            {
                Id = conversationModel.Id,
                FacebookConversationId = conversationModel.FacebookConversationId,
                ProcessInstanceId = conversationModel.ProcessInstanceId,
                CustomerId = conversationModel.CustomerId,
                AssignedEmployeeId = conversationModel.AssignedEmployeeId
            };

            return Ok(conversation);
        }

        [HttpPost]
        [Route("close")]
        public async Task<IHttpActionResult> CloseConversation(ConversationCloseViewModel conversation)
        {
            var taskResult = await _activitiWorker.GetCurrentTaskForInstance(conversation.ProcessInstanceId);

            if (taskResult.IsError)
                return InternalServerError();

            await _activitiWorker.CompleteTaskAndGetNextAsync(conversation.ProcessInstanceId, taskResult.Value.Id);

            var convertsationModel = new ConversationModel
            {
                Id = conversation.Id,
                CustomerId = null,
                AssignedEmployeeId = null,
                ProcessInstanceId = null,
                ProcessTask = TalkProcessTask.None
            };

            _conversationService.Update(convertsationModel);

            return Ok();
        }
    }
}
