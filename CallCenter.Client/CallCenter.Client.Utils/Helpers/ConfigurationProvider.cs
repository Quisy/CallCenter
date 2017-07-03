using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Utils.Helpers.Interfaces;

namespace CallCenter.Client.Utils.Helpers
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public System.Configuration.Configuration Configuration { get; set; }

        public ConfigurationProvider()
        {
            Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
    }
}
