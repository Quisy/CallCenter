using System.Data.Entity;
using System.Linq;
using CallCenter.API.Domain.DataContext;
using CallCenter.API.Repository.Base;
using CallCenter.API.Repository.Interfaces.Conversation;
using CallCenter.API.Utils;

namespace CallCenter.API.Repository.Conversation
{
    public class ConversationRepository : CrudRepository<CallCenterContext, DomainModel.DomainModels.Conversation>, IConversationRepository
    {
        protected override IQueryable<DomainModel.DomainModels.Conversation> QueryAll(CallCenterContext context)
        {
            return base.QueryAll(context).Include(c=>c.AssignedEmployee).Include(c => c.Messages);
        }

        public Result<DomainModel.DomainModels.Conversation> GetConversationByFacebookCnversationId(string facebookConversationId)
        {
            using (var context = new CallCenterContext())
            {
                var conversation =
                    context.Conversations.SingleOrDefault(c => c.FacebookConversationId.Equals(facebookConversationId));

                return Result<DomainModel.DomainModels.Conversation>.ErrorWhenNoData(conversation);
            }
        }
    }
}
