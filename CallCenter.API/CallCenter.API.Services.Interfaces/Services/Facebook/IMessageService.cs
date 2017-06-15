using System.Collections.Generic;
using System.Threading.Tasks;
using CallCenter.API.Models.Facebook;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Facebook
{
    public interface IMessageService : IFacebookService
    {
        Task<Result<IList<MessageModel>>> GetConversationMessages(string conversationId);
        Task<Result<MessageModel>> SendMessageToConversation(string conversationId, string message);
    }
}
