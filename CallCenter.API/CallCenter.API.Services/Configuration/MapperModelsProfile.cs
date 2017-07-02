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
            CreateMap<Models.Conversation.MessageModel, DomainModel.DomainModels.Message>().MaxDepth(1);
            CreateMap<Models.Membership.EmployeeModel, DomainModel.DomainModels.Employee>().MaxDepth(1);
            CreateMap<Models.Membership.CustomerModel, DomainModel.DomainModels.Customer>().MaxDepth(1);

            CreateMap<DomainModel.DomainModels.Conversation, Models.Conversation.ConversationModel>().MaxDepth(1);
            CreateMap<DomainModel.DomainModels.Message, Models.Conversation.MessageModel>().MaxDepth(1);
            CreateMap<DomainModel.DomainModels.Employee, Models.Membership.EmployeeModel>().MaxDepth(1);
            CreateMap<DomainModel.DomainModels.Customer, Models.Membership.CustomerModel>().MaxDepth(1);
        }
    }
}
