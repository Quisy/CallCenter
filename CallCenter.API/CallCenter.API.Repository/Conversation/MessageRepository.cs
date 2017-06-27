using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Domain.DataContext;
using CallCenter.API.Repository.Base;
using CallCenter.API.Repository.Interfaces.Conversation;

namespace CallCenter.API.Repository.Conversation
{
    public class MessageRepository : CrudRepository<CallCenterContext, DomainModel.DomainModels.Message>, IMessageRepository
    {
        protected override IQueryable<DomainModel.DomainModels.Message> QueryAll(CallCenterContext context)
        {
            return base.QueryAll(context).Include(m => m.Conversation).Include(m => m.Author);
        }
    }
}
