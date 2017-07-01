using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CallCenter.API.Services.Configuration
{
    public class MapperModelsProfile : Profile
    {
        public MapperModelsProfile()
        {
            CreateMap<Models.Conversation.ConversationModel, DomainModel.DomainModels.Conversation>().MaxDepth(1);
            CreateMap<Models.Conversation.MessageModel, DomainModel.DomainModels.Message>();
            CreateMap<Models.Membership.EmployeeModel, DomainModel.DomainModels.Employee>();
            CreateMap<Models.Membership.CustomerModel, DomainModel.DomainModels.Customer>();

            CreateMap<DomainModel.DomainModels.Conversation, Models.Conversation.ConversationModel>().MaxDepth(1);
            CreateMap<DomainModel.DomainModels.Message, Models.Conversation.MessageModel>();
            CreateMap<DomainModel.DomainModels.Employee, Models.Membership.EmployeeModel>();
            CreateMap<DomainModel.DomainModels.Customer, Models.Membership.CustomerModel>();
        }
    }
}
