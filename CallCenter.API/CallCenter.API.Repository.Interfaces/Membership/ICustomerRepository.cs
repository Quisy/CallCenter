using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Repository.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Repository.Interfaces.Membership
{
    public interface ICustomerRepository : ICrudRepository<DomainModel.DomainModels.Customer>
    {
        Result<Customer> GetCustomerByApplicationUserId(string id);
    }
}
