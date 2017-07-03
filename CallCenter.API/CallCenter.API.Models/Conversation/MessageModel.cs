using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.Models.Conversation
{
    public class MessageModel
    {
        public int Id { get; set; }

        public int ConversationId { get; set; }

        public string FacebookMessageId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public bool IsSystemMessage { get; set; }

        //public ApplicationUser Author { get; set; }
        public ConversationModel Conversation { get; set; }
    }
}
