namespace CallCenter.API.Services.Interfaces.Base
{
    public interface IFacebookService : IApiService
    {
        string AccessToken { get; }
    }
}
