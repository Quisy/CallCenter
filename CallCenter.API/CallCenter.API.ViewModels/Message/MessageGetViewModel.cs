using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.ViewModels.Message
{
    public class MessageGetViewModel
    {
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public DateTime Date { get; set; }

        public int ConversationId { get; set; }
    }
}
