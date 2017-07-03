using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Models;
using CallCenter.Client.Services.Base;
using CallCenter.Client.Services.Interfaces.Services;
using CallCenter.Client.Utils.Helpers.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CallCenter.Client.Services.Services
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(IAppSettingsProvider appSettingsProvider) : base(appSettingsProvider)
        {
           
        }

        public async Task<UserModel> Login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                var requestdata = $"grant_type=password&username={username}&password={password}";
                var requestMessage =
                    new HttpRequestMessage { Content = new StringContent(requestdata, Encoding.UTF8) };
                try
                {
                    var response = await client.PostAsync("/token", requestMessage.Content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = (JObject)JsonConvert.DeserializeObject(responseString);

                    if (response.IsSuccessStatusCode)
                    {
                        return new UserModel
                        {
                            Token = data["access_token"].ToString()
                        };
                    }

                    return null;
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return null;
                }
            }
        }
    }
}

