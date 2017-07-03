using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.ViewModels.Conversation
{
    public class ConversationGetViewModel
    {
        public int Id { get; set; }
        public string FacebookConversationId { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public int? ProcessInstanceId { get; set; }
        public int? CustomerId { get; set; }
    }
}
