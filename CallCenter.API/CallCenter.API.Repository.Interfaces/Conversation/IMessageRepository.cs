using System.Collections.Generic;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Repository.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Repository.Interfaces.Conversation
{
    public interface IMessageRepository : ICrudRepository<DomainModel.DomainModels.Message>
    {
        Result<IList<Message>> GetMessagesToForConversation(int conversationId);
    }
}
