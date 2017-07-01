using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Activiti
{
    public interface ITaskService : IActivitiService
    {
        Task<Result<TaskModel>> GetCurrentTaskForInstanceByIdAsync(int instanceId);

        Task<Result<bool>> CompleteTaskAsync(string taskId);
    }
}
