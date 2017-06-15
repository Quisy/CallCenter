namespace CallCenter.API.Services.Interfaces.Base
{
    public interface IActivitiService : IApiService
    {
        string GetBasicAuthorizationHeaderValue();
    }
}
