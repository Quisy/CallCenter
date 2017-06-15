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
    public class ProcessDefinitionService : ActivitiService, IProcessDefinitionService
    {
        private const string RequestUri = "repository/process-definitions/";

        public async Task<Result<ProcessDefinitionModel>> GetProcessDefinitionByNameAsync(string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, RequestUri);
              
                requestMessage.Headers.Add("Authorization", base.GetBasicAuthorizationHeaderValue());

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return Result<ProcessDefinitionModel>.Error(response.ReasonPhrase);

                var responseString = await response.Content.ReadAsStringAsync();

                var processDefinitions = JsonConvert.DeserializeObject<List<ProcessDefinitionModel>>(responseString);

                var result = processDefinitions.SingleOrDefault(x => x.Name.Equals(name));

                return Result<ProcessDefinitionModel>.ErrorWhenNoData(result);
            }
        }
    }
}
