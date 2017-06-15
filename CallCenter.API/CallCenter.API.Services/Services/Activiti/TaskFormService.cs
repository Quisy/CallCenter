using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Activiti;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;
using Newtonsoft.Json;

namespace CallCenter.API.Services.Services.Activiti
{
    public class TaskFormService : ActivitiService, ITaskFormService
    {
        private const string RequestUri = "form/form-data";

        public TaskFormService(ISettingsManager settingsManager) : base(settingsManager)
        {
        }

        public async Task<Result<bool>> SubmitTaskFormDataAsync(TaskFormModel taskFormModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, RequestUri);

                requestMessage.Headers.Add("Authorization", base.GetBasicAuthorizationHeaderValue());

                string jsonData = JsonConvert.SerializeObject(taskFormModel);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return null;

                return Result<bool>.ErrorWhenNoData(true);
            }
        }

       
    }
}
