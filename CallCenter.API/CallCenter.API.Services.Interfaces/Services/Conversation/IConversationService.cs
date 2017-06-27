using System.Threading.Tasks;
using CallCenter.API.Models.Conversation;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Conversation
{
    public interface IConversationService : ICrudService<ConversationModel>
    {
        Result<ConversationModel> GetConversationByFacebookCnversationId(string facebookConversationId);
    }
}
