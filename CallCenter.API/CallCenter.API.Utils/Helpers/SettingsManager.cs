using System.Configuration;
using CallCenter.API.Utils.Helpers.Interfaces;

namespace CallCenter.API.Utils.Helpers
{
    public class SettingsManager : ISettingsManager
    {
        public string Load(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
