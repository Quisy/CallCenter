﻿using System.ComponentModel.DataAnnotations;
using CallCenter.API.DomainModel.Base;

namespace CallCenter.API.DomainModel.DomainModels
{
    public class Customer : KeyEntity
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public decimal AccountBalance { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
