using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CallCenter.API.Models.Facebook
{
    public class ConversationModel
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "message_count")]
        public int MessageCount { get; set; }

        [JsonProperty(PropertyName = "unread_count")]
        public int UnreadCount { get; set; }
    }
}
