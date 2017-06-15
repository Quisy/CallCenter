using CallCenter.API.Services.Interfaces.Base;

namespace CallCenter.API.Services.Base
{
    public abstract class ApiService : BaseService, IApiService
    {
        public string BaseUrl { get; set; }

        public string GetBasicAuthorizationHeaderValue(string username, string password)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
        }
    }
}
