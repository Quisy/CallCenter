using System.Configuration;
using CallCenter.Client.Services.Interfaces;
using CallCenter.Client.Utils.Helpers.Interfaces;

namespace CallCenter.Client.Services.Base
{
    public abstract class BaseService : IService
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        public string ApiUrl { get; set; }
        public string UserToken => _appSettingsProvider.Load(Constants.AppSettings.UserTokenKeyName);

        protected BaseService(IAppSettingsProvider appSettingsProvider)
        {
            _appSettingsProvider = appSettingsProvider;
            ApiUrl = _appSettingsProvider.Load(Constants.AppSettings.ApiPathKeyName);
        }

    }
}
