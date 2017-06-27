using CallCenter.API.Repository.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Repository.Interfaces.Conversation
{
    public interface IConversationRepository : ICrudRepository<DomainModel.DomainModels.Conversation>
    {
        Result<DomainModel.DomainModels.Conversation> GetConversationByFacebookCnversationId(string facebookConversationId);
    }
}
