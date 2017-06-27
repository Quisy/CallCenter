using CallCenter.API.Models.Conversation;
using CallCenter.API.Repository.Interfaces.Conversation;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Conversation;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Services.Services.Conversation
{
    public class ConversationService : CrudService<ConversationModel, IConversationRepository, DomainModel.DomainModels.Conversation>, IConversationService
    {
        public ConversationService(IConversationRepository repository, IModelMapper modelMapper) : base(repository, modelMapper)
        {
        }

        public Result<ConversationModel> GetConversationByFacebookCnversationId(string facebookConversationId)
        {
            var conversationResult = Repository.GetConversationByFacebookCnversationId(facebookConversationId);

            return Result<ConversationModel>.WarningWhenNoData(ModelMapper.MapSingle<DomainModel.DomainModels.Conversation, ConversationModel>(conversationResult.Value));
        }
    }
}
