using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Models.Conversation;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Conversation
{
    public interface IMessageService : ICrudService<MessageModel>
    {
        Result<MessageModel> AddMessageToConversation(int conversationId, string message, string fbMessageId);

        Result<IList<MessageModel>> GetMessagesToForConversation(int conversationId);
    }
}
