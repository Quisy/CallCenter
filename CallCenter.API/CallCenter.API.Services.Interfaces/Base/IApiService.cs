namespace CallCenter.API.Services.Interfaces.Base
{
    public interface IApiService : IBaseService
    {
        string GetBasicAuthorizationHeaderValue(string username, string password);
    }
}
