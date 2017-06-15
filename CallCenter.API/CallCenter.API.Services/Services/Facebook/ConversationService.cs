using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CallCenter.API.Models.Activiti;
using CallCenter.API.Models.Facebook;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Facebook;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;
using Newtonsoft.Json;

namespace CallCenter.API.Services.Services.Facebook
{
    public class ConversationService : FacebookService, IConversationService
    {
        public ConversationService(ISettingsManager settingsManager) : base(settingsManager)
        {
        }

        public async Task<Result<IList<ConversationModel>>> GetPageConversationsWithUnreadMessages(string pageId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                string requestUrl = $"{pageId}/Conversations?access_token={AccessToken}";
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return Result<IList<ConversationModel>>.Error(response.ReasonPhrase);

                var responseString = await response.Content.ReadAsStringAsync();

                var conversations = JsonConvert.DeserializeObject<IList<ConversationModel>>(responseString);

                var result = conversations.Where(x => x.UnreadCount > 0).ToList();

                return Result<IList<ConversationModel>>.ErrorWhenNoData(result);
            }
        }
    }
}
