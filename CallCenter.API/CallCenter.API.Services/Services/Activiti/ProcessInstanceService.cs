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
using CallCenter.API.Utils.Helpers.Interfaces;
using Newtonsoft.Json;

namespace CallCenter.API.Services.Services.Activiti
{
    public class ProcessInstanceService : ActivitiService, IProcessInstanceService
    {
        private const string RequestUri = "runtime/process-instances/";

        public ProcessInstanceService(ISettingsManager settingsManager) : base(settingsManager)
        {
        }

        public async Task<Result<ProcessInstanceModel>> StartProcessInstanceByProcessDefinitionIdAsync(string processDefinitionId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, RequestUri);

                requestMessage.Headers.Add("Authorization", base.GetBasicAuthorizationHeaderValue());

                var requestBodyModel = new ProcessInstanceModel{ ProcessDefinitionId = processDefinitionId };

                string jsonData = JsonConvert.SerializeObject(requestBodyModel);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return Result<ProcessInstanceModel>.Error(response.ReasonPhrase);

                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ProcessInstanceModel>(responseString);

                return Result<ProcessInstanceModel>.ErrorWhenNoData(result);
            }
        }

      
    }
}
