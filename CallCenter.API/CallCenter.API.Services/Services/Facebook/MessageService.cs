﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CallCenter.API.Models.Facebook;
using CallCenter.API.Services.Base;
using CallCenter.API.Services.Interfaces.Services.Facebook;
using CallCenter.API.Utils;
using CallCenter.API.Utils.Helpers.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CallCenter.API.Services.Services.Facebook
{
    public class MessageService : FacebookService, IMessageService
    {
        public MessageService(ISettingsManager settingsManager) : base(settingsManager)
        {
        }

        public async Task<Result<IList<MessageModel>>> GetConversationMessages(string conversationId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                string requestUrl = $"{conversationId}/messages?fields=message,created_time&access_token={AccessToken}";
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return Result<IList<MessageModel>>.Error(response.ReasonPhrase);

                var responseString = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(responseString);
                var result = JsonConvert.DeserializeObject<IList<MessageModel>>(data["data"].ToString());

                return Result<IList<MessageModel>>.ErrorWhenNoData(result);
            }
        }

        public async Task<Result<MessageModel>> SendMessageToConversation(string conversationId, string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base.BaseUrl);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{conversationId}/messages?access_token={AccessToken}");

                var values = new Dictionary<string, string>
                {
                    { "message", message }
                };

                var content = new FormUrlEncodedContent(values);
                requestMessage.Content = content;

                var response = await client.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                    return Result<MessageModel>.Error(response.ReasonPhrase);

                var responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MessageModel>(responseString);

                return Result<MessageModel>.ErrorWhenNoData(result);
            }
        }
    }
}
