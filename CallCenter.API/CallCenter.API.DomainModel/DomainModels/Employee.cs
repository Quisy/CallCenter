﻿using CallCenter.API.DomainModel.Base;
using CallCenter.API.Enums;

namespace CallCenter.API.DomainModel.DomainModels
{
    public class Employee : KeyEntity
    {
        public string ApplicationUserId { get; set; }
        public EmployeeStatus Status { get; set; }
        public string ConversationId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}