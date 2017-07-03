using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Utils.Helpers.Interfaces;

namespace CallCenter.Client.Utils.Helpers
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly System.Configuration.Configuration _configuration;


        public AppSettingsProvider(IConfigurationProvider configurationProvider)
        {
            _configuration = configurationProvider.Configuration;
        }

        public void Save()
        {
            //_configuration.AppSettings.Settings[nameof(ReleaseDirectoryPath)].Value = ReleaseDirectoryPath;
            _configuration.Save(ConfigurationSaveMode.Modified);
        }

        public void Load()
        {
            //ReleaseDirectoryPath = _configuration.AppSettings.Settings[nameof(ReleaseDirectoryPath)].Value;
        }
    }
}
