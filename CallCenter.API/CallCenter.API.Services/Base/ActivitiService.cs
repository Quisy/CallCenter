using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Services.Interfaces.Base;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Services.Base
{
    public abstract class ActivitiService : ApiService, IActivitiService
    {
        private readonly string _activitiUsername;
        private readonly string _activitiPassword;

        protected ActivitiService(ISettingsManager settingsManager)
        {
            var settingsManager1 = settingsManager;

            BaseUrl = settingsManager1.Load(Constants.SettingsConstants.ActivitiPathKeyName);
            _activitiUsername = settingsManager1.Load(Constants.SettingsConstants.ActivitiUsernameKeyName);
            _activitiPassword = settingsManager1.Load(Constants.SettingsConstants.ActivitiPasswordKeyName);
        }

        public string GetBasicAuthorizationHeaderValue()
        {
            return base.GetBasicAuthorizationHeaderValue(_activitiUsername, _activitiPassword);
        }
    }
}
