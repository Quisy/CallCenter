using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Models;
using CallCenter.Client.Services.Base;
using CallCenter.Client.Services.Interfaces.Services;
using CallCenter.Client.Utils.Helpers.Interfaces;
using Newtonsoft.Json;

namespace CallCenter.Client.Services.Services
{
    public class ConversationService : BaseService, IConversationService
    {
        public ConversationService(IAppSettingsProvider appSettingsProvider) : base(appSettingsProvider)
        {
        }

        public async Task<ConversationModel> FindConversationForEmployee(int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"conversation?employeeId={employeeId}");
                requestMessage.Headers.Add("Authorization", "bearer " + UserToken);

                var responseString = string.Empty;

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return null;


                responseString = await response.Content.ReadAsStringAsync();

                ConversationModel data = JsonConvert.DeserializeObject<ConversationModel>(responseString);

                return data;
            }
        }

        public async Task CloseConversation(ConversationModel conversation)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "conversation/close");
                requestMessage.Headers.Add("Authorization", "bearer " + UserToken);

                string jsonData = JsonConvert.SerializeObject(conversation);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                await client.SendAsync(requestMessage);
            }
        }
    }
}
