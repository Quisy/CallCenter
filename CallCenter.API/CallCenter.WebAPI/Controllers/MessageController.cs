using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CallCenter.API.Services.Interfaces.Services.Conversation;
using CallCenter.API.ViewModels.Message;
using CallCenter.API.Web.Controllers.Base;

namespace CallCenter.API.Web.Controllers
{
    [Authorize]
    [RoutePrefix("message")]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;

        private readonly Services.Interfaces.Services.Facebook.IMessageService _facebbokMessageService;

        public MessageController(IMessageService messageService, Services.Interfaces.Services.Facebook.IMessageService facebbokMessageService, IConversationService conversationService)
        {
            _messageService = messageService;
            _facebbokMessageService = facebbokMessageService;
            _conversationService = conversationService;
        }

        [HttpGet]
        public IHttpActionResult GetMessagesAfterDate(int conversationId, DateTime date)
        {
            var messagesResult = _messageService.GetMessagesForConversationFromDate(conversationId, date);

            if (messagesResult.IsError)
                return NotFound();

            var messagesModel = messagesResult.Value;

            var resultMessages = new List<MessageGetViewModel>();

            foreach (var model in messagesModel)
            {
                resultMessages.Add(new MessageGetViewModel
                {
                    ConversationId = conversationId,
                    Content = model.Content,
                    Date = model.Date,
                    AuthorId = model.AuthorId
                });
            }

            return Ok(resultMessages);
        }

        [HttpPost]
        public async Task<IHttpActionResult> SendMessage(MessageAddViewModel message)
        {
            var conversationResult = _conversationService.GetById(message.ConversationId);

            if (conversationResult.IsError)
                return NotFound();

            var conversation = conversationResult.Value;

            var fbMesageResult = await _facebbokMessageService.SendMessageToConversation(conversation.FacebookConversationId, message.Content);

            if (fbMesageResult.IsError)
                return InternalServerError();

            var fbMessage = fbMesageResult.Value;

            _messageService.AddMessageToConversation(message.ConversationId,
                message.Content, fbMessage.Id);

            return Ok();
        }
    }
}
