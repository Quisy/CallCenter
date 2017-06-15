using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Services.Base
{
    public abstract class FacebookService : ApiService, IFacebookService
    {
        public string AccessToken { get; private set; }

        protected FacebookService(ISettingsManager settingsManager)
        {
            BaseUrl = settingsManager.Load(Constants.SettingsConstants.FacebookApiPathKeyName);

            AccessToken = settingsManager.Load(Constants.SettingsConstants.FacebookAccessTokenKeyName);
        }
    }
}
