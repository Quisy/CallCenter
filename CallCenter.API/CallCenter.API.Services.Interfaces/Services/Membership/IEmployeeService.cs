using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Models.Conversation;
using CallCenter.API.Models.Membership;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Membership
{
    public interface IEmployeeService : ICrudService<EmployeeModel>
    {
        Result<EmployeeModel> GetAvailableEmployee();
    }
}
