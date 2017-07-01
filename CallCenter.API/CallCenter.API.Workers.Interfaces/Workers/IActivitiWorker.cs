using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Utils;

namespace CallCenter.API.Workers.Interfaces.Workers
{
    public interface IActivitiWorker
    {
        Task<Result<ProcessInstanceModel>> StartInstanceOfProcessDefinitionAsync(string processDefinitionName);
        Task<Result<TaskModel>> CompleteTaskSubmittingFormAndGetNextAsync(int processInstanceId, TaskFormModel taskFormModel);
        Task<Result<TaskModel>> CompleteTaskAndGetNextAsync(int processInstanceId, string taskId);
        Task<Result<TaskModel>> GetCurrentTaskForInstance(int processInstanceId);
    }
}
