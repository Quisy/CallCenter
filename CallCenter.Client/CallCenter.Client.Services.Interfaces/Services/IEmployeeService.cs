using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Enums;

namespace CallCenter.Client.Services.Interfaces.Services
{
    public interface IEmployeeService : IService
    {
        Task<int> GetEmployeeId();

        Task<bool> UpdateStatus(int employeeId, EmployeeStatus status);
    }
}
