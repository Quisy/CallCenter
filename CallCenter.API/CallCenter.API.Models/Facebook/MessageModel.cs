using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CallCenter.API.Models.Facebook
{
    public class MessageModel
    {
        public string Id { get; set; }

        public string Message { get; set; }

        [JsonProperty(PropertyName = "created_time")]
        public DateTime CreatedTime { get; set; }
    }
}
