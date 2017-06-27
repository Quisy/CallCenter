using System.Collections.Generic;
using CallCenter.API.DomainModel.Base;
using CallCenter.API.Enums;

namespace CallCenter.API.DomainModel.DomainModels
{
    public class Conversation : KeyEntity
    {
        public string FacebookConversationId { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public ConversationStatus Status { get; set; }
        public TalkProcessTask ProcessTask { get; set; }
        public int? ProcessInstanceId { get; set; }

        public Employee AssignedEmployee { get; set; }
        public IList<Message> Messages { get; set; }

        public Conversation()
        {
            Messages = new List<Message>();
        }
    }
}
