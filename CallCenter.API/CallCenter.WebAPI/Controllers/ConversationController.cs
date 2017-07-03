using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CallCenter.API.Services.Interfaces.Services.Conversation;
using CallCenter.API.ViewModels.Conversation;
using CallCenter.API.Web.Controllers.Base;
using Microsoft.AspNet.Identity;

namespace CallCenter.API.Web.Controllers
{
    [Authorize]
    [RoutePrefix("conversation")]
    public class ConversationController : BaseController
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
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
    }
}
