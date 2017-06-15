using System;
using CallCenter.API.DomainModel.Base;

namespace CallCenter.API.DomainModel.DomainModels
{
    public class Message : KeyEntity
    {
        public string ConversationId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
