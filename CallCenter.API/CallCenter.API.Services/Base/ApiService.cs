using CallCenter.API.Services.Interfaces.Base;

namespace CallCenter.API.Services.Base
{
    public abstract class ApiService : IApiService
    {
        public string BaseUrl { get; set; }
    }
}
