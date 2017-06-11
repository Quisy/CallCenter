using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Activiti;
using CallCenter.API.Utils;
using Newtonsoft.Json;

namespace CallCenter.API.Services.Services.Activiti
{
    public class TaskService : ApiService, ITaskService
    {
        private const string RequestUri = "runtime/tasks/";

        public async Task<Result<TaskModel>> GetCurrentTaskForInstanceByIdAsync(string instanceId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, RequestUri);

                requestMessage.Headers.Add("Authorization", base.GetBasicAuthorizationHeaderValue());

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return null;

                var responseString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseString);

                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(responseString);

                var result = tasks.SingleOrDefault(x => x.ProcessInstanceId.Equals(instanceId));

                return Result<TaskModel>.ErrorWhenNoData(result);
            }
        }
    }
}
