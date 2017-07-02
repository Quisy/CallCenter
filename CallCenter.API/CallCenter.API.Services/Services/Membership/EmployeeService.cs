using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Models.Membership;
using CallCenter.API.Repository.Interfaces.Membership;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Membership;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Services.Services.Membership
{
    public class EmployeeService : CrudService<EmployeeModel, IEmployeeRepository, DomainModel.DomainModels.Employee>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository repository, IModelMapper modelMapper) : base(repository, modelMapper)
        {
        }

        public Result<EmployeeModel> GetAvailableEmployee()
        {
            var employeesResult = Repository.GetAvailableEmployees();

            if (employeesResult.IsWarning)
                return Result<EmployeeModel>.Warning(null, employeesResult.Messages);

            return Result<EmployeeModel>.WarningWhenNoData(ModelMapper.MapSingle<Employee, EmployeeModel>(employeesResult.Value.FirstOrDefault()));
        }
    }
}
