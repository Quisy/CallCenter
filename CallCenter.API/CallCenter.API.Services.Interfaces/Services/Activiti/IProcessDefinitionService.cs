using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Activiti
{
    public interface IProcessDefinitionService : IActivitiService
    {
        Task<Result<ProcessDefinitionModel>> GetProcessDefinitionByNameAsync(string name);
    }
}
