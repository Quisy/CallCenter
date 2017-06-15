using System.Collections.Generic;
using System.Threading.Tasks;
using CallCenter.API.Models.Facebook;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Facebook
{
    public interface IConversationService : IFacebookService
    {
        Task<Result<IList<ConversationModel>>> GetPageConversationsWithUnreadMessages(string pageId);
    }
}
