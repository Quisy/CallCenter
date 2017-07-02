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
    public interface IEmployeeRepository : ICrudRepository<DomainModel.DomainModels.Employee>
    {
        Result<IList<Employee>> GetAvailableEmployees();
    }
}
