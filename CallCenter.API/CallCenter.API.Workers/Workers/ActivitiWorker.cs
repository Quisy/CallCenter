using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Services.Interfaces.Services.Activiti;
using CallCenter.API.Utils;
using CallCenter.API.Workers.Interfaces.Workers;

namespace CallCenter.API.Workers.Workers
{
    public class ActivitiWorker : IActivitiWorker
    {
        private readonly IProcessDefinitionService _processDefinitionService;
        private readonly IProcessInstanceService _processInstanceService;
        private readonly ITaskService _taskService;
        private readonly ITaskFormService _taskFormService;

        public ActivitiWorker(IProcessDefinitionService processDefinitionService, IProcessInstanceService processInstanceService, ITaskService taskService, ITaskFormService taskFormService)
        {
            _processDefinitionService = processDefinitionService;
            _processInstanceService = processInstanceService;
            _taskService = taskService;
            _taskFormService = taskFormService;
        }


        public async Task<Result<ProcessInstanceModel>> StartInstanceOfProcessDefinitionAsync(string processDefinitionName)
        {
            var processDefinitionResult = await _processDefinitionService.GetProcessDefinitionByNameAsync(processDefinitionName);

            if(processDefinitionResult.IsError)
                return Result<ProcessInstanceModel>.Error(processDefinitionResult.Messages);

            var processInstanceResult = await _processInstanceService.StartProcessInstanceByProcessDefinitionIdAsync(processDefinitionResult.Value.Id);

            if (processInstanceResult.IsError)
                return Result<ProcessInstanceModel>.Error(processInstanceResult.Messages);

            return Result<ProcessInstanceModel>.ErrorWhenNoData(processInstanceResult.Value);
        }

        public async Task<Result<TaskModel>> CompleteTaskSubmittingFormAndGetNextAsync(string processInstanceId, TaskFormModel taskFormModel)
        {
            var formSubmitResult = await _taskFormService.SubmitTaskFormDataAsync(taskFormModel);

            if (formSubmitResult.IsError)
                return Result<TaskModel>.Error(formSubmitResult.Messages);

            return await this.CompleteTaskAndGetNextAsync(processInstanceId, taskFormModel.TaskId);
        }

        public async Task<Result<TaskModel>> CompleteTaskAndGetNextAsync(string processInstanceId, string taskId)
        {
            var completeTaskResult = await _taskService.CompleteTaskAsync(taskId);

            if (completeTaskResult.IsError)
                return Result<TaskModel>.Error(completeTaskResult.Messages);

            var nextTaskResult = await _taskService.GetCurrentTaskForInstanceByIdAsync(processInstanceId);

            if (nextTaskResult.IsError)
                return Result<TaskModel>.Error(nextTaskResult.Messages);

            return Result<TaskModel>.ErrorWhenNoData(nextTaskResult.Value);
        }
    }
}
