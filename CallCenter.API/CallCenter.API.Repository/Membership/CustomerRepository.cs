using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Domain.DataContext;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Repository.Base;
using CallCenter.API.Repository.Interfaces.Membership;
using CallCenter.API.Utils;

namespace CallCenter.API.Repository.Membership
{
    public class CustomerRepository : CrudRepository<CallCenterContext, DomainModel.DomainModels.Customer>, ICustomerRepository
    {
        public Result<Customer> GetCustomerByApplicationUserId(string id)
        {
            using (var context = new CallCenterContext())
            {
                var customer = context.Customers.SingleOrDefault(c => c.ApplicationUserId.Equals(id));

                return Result<Customer>.ErrorWhenNoData(customer);
            }
        }
    }
}
