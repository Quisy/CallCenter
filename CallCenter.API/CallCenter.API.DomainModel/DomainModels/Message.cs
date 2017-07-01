using System;
using System.ComponentModel.DataAnnotations;
using CallCenter.API.DomainModel.Base;

namespace CallCenter.API.DomainModel.DomainModels
{
    public class Message : KeyEntity
    {
        public int ConversationId { get; set; }

        public string FacebookMessageId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
        public Conversation Conversation { get; set; }
    }
}
