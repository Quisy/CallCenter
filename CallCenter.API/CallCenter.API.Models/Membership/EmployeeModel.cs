using System.Collections.Generic;
using CallCenter.API.Enums;
using CallCenter.API.Models.Facebook;

namespace CallCenter.API.Models.Membership
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public EmployeeStatus Status { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }
        public IList<ConversationModel> Conversations { get; set; }
    }
}
