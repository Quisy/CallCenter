using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils;

namespace CallCenter.API.Services.Interfaces.Services.Activiti
{
    public interface ITaskFormService : IActivitiService
    {
        Task<Result<bool>> SubmitTaskFormDataAsync(TaskFormModel taskFormModel);
    }
}
