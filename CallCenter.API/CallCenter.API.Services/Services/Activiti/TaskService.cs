﻿using System;
using System.Collections.Generic;
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
using Newtonsoft.Json.Linq;

namespace CallCenter.API.Services.Services.Activiti
{
    public class TaskService : ActivitiService, ITaskService
    {
        private const string RequestUri = "service/runtime/tasks";

        public TaskService(ISettingsManager settingsManager) : base(settingsManager)
        {
        }

        public async Task<Result<TaskModel>> GetCurrentTaskForInstanceByIdAsync(int instanceId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, RequestUri);

                requestMessage.Headers.Add("Authorization", base.GetBasicAuthorizationHeaderValue());

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return Result<TaskModel>.Error(response.ReasonPhrase);

                var responseString = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(responseString);
                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(data["data"].ToString());

                var result = tasks.SingleOrDefault(x => x.ProcessInstanceId.Equals(instanceId.ToString()));

                return Result<TaskModel>.ErrorWhenNoData(result);
            }
        }

        public async Task<Result<bool>> CompleteTaskAsync(string taskId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{RequestUri}/{taskId}");

                requestMessage.Headers.Add("Authorization", base.GetBasicAuthorizationHeaderValue());

                string jsonData = @"{""action"" : ""complete""}";
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return null;

                return Result<bool>.ErrorWhenNoData(true);
            }
        }
    }
}
