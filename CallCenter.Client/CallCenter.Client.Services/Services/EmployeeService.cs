using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Enums;
using CallCenter.Client.Services.Base;
using CallCenter.Client.Services.Interfaces.Services;
using CallCenter.Client.Utils.Helpers.Interfaces;
using Newtonsoft.Json;

namespace CallCenter.Client.Services.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(IAppSettingsProvider appSettingsProvider) : base(appSettingsProvider)
        {
        }

        public async Task<int> GetEmployeeId()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "employee/id");
                requestMessage.Headers.Add("Authorization", "bearer " + UserToken);

                var responseString = string.Empty;

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return 0;


                responseString = await response.Content.ReadAsStringAsync();

                return int.Parse(responseString);
            }
        }

        public async Task<bool> UpdateStatus(int employeeId, EmployeeStatus status)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, $"employee/status?employeeId={employeeId}&status={(int)status}");
                requestMessage.Headers.Add("Authorization", "bearer " + UserToken);

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return false;


                return true;
            }
        }

    }
}
