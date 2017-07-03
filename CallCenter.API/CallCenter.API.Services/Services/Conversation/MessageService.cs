using System;
using System.Collections.Generic;
using System.Linq;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Models.Conversation;
using CallCenter.API.Repository.Interfaces.Conversation;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Conversation;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;


namespace CallCenter.API.Services.Services.Conversation
{
    public class MessageService : CrudService<MessageModel, IMessageRepository, DomainModel.DomainModels.Message>, IMessageService
    {
        public MessageService(IMessageRepository repository, IModelMapper modelMapper) : base(repository, modelMapper)
        {
        }

        public Result<MessageModel> AddMessageToConversation(int conversationId, string message, string fbMessageId, string authorId = null)
        {
            var messageModel = new MessageModel
            {
                ConversationId = conversationId,
                Content = message,
                FacebookMessageId = fbMessageId,
                Date = DateTime.Now,
                AuthorId = authorId,
                IsSystemMessage = authorId == null
            };

            return base.Add(messageModel);
        }

        public Result<IList<MessageModel>> GetMessagesToForConversation(int conversationId)
        {
            var result = Repository.GetMessagesToForConversation(conversationId);
            if (result.IsError)
                return Result<IList<MessageModel>>.Error(result.Messages);

            var messages = ModelMapper.MapList<Message, MessageModel>(result.Value);
            return Result<IList<MessageModel>>.ErrorWhenNoData(messages);
        }

        public Result<IList<MessageModel>> GetMessagesForConversationFromDate(int conversationId, DateTime date)
        {
            var result = Repository.GetMessagesToForConversation(conversationId);
            if (result.IsError)
                return Result<IList<MessageModel>>.Error(result.Messages);

            var messages = ModelMapper.MapList<Message, MessageModel>(result.Value);
            return Result<IList<MessageModel>>.WarningWhenNoData(messages.Where(m => m.Date > date).ToList());
        }
    }
}
