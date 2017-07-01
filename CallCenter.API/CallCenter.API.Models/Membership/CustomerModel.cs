using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.Models.Membership
{
    public class CustomerModel
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public decimal AccountBalance { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }
    }
}
