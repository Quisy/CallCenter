using System;
using System.ComponentModel.DataAnnotations;
using CallCenter.API.DomainModel.Base;

namespace CallCenter.API.DomainModel.DomainModels
{
    public class Message : KeyEntity
    {
        [Required]
        public string ConversationId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
