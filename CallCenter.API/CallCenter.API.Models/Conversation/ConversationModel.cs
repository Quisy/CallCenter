using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Enums;
using CallCenter.API.Models.Membership;

namespace CallCenter.API.Models.Conversation
{
    public class ConversationModel
    {
        public int Id { get; set; }
        public string FacebookConversationId { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public ConversationStatus Status { get; set; }
        public TalkProcessTask ProcessTask { get; set; }
        public int? ProcessInstanceId { get; set; }
        public int? CustomerId { get; set; }

        public EmployeeModel AssignedEmployee { get; set; }
        public CustomerModel Customer { get; set; }
        public IList<MessageModel> Messages { get; set; }
    }
}
