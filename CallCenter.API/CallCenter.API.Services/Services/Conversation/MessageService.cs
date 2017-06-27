using CallCenter.API.Models.Conversation;
using CallCenter.API.Repository.Interfaces.Conversation;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Conversation;
using CallCenter.API.Utils.Helpers.Interfaces;


namespace CallCenter.API.Services.Services.Conversation
{
    public class MessageService : CrudService<MessageModel, IMessageRepository, DomainModel.DomainModels.Message>, IMessageService
    {
        public MessageService(IMessageRepository repository, IModelMapper modelMapper) : base(repository, modelMapper)
        {
        }
    }
}
