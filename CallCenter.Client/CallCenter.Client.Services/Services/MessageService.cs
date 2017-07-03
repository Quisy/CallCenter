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
    public class MessageService : BaseService, IMessageService
    {
        public MessageService(IAppSettingsProvider appSettingsProvider) : base(appSettingsProvider)
        {
        }

        public async Task<IList<MessageModel>> GetAllMessagesForConversation(int conversationId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"message/all?conversationId={conversationId}");
                requestMessage.Headers.Add("Authorization", "bearer " + UserToken);

                var responseString = string.Empty;

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return null;


                responseString = await response.Content.ReadAsStringAsync();

                IList<MessageModel> data = JsonConvert.DeserializeObject<IList<MessageModel>>(responseString);

                return data;
            }
        }

        public async Task<MessageModel> SendMessage(int conversationId, string content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"message");
                requestMessage.Headers.Add("Authorization", "bearer " + UserToken);

                string jsonData = @"{""Content"":""" + content + @""", ""ConversationId"":""" + conversationId + @"""}";
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return null;


                var responseString = await response.Content.ReadAsStringAsync();

                MessageModel data = JsonConvert.DeserializeObject<MessageModel>(responseString);

                return data;
            }
        }
    }
}
