using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Domain.DataContext;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Enums;
using CallCenter.API.Repository.Base;
using CallCenter.API.Repository.Interfaces.Membership;
using CallCenter.API.Utils;

namespace CallCenter.API.Repository.Membership
{
    public class EmployeeRepository : CrudRepository<CallCenterContext, DomainModel.DomainModels.Employee>, IEmployeeRepository
    {
        public Result<IList<Employee>> GetAvailableEmployees()
        {
            using (var context = new CallCenterContext())
            {
                var employees = context.Employees.Where(e => e.Status == EmployeeStatus.Available).ToList();

                return Result<IList<Employee>>.WarningWhenNoData(employees);
            }
        }
    }
}
