using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.Client.Utils.Helpers.Interfaces
{
    public interface IConfigurationProvider
    {
        System.Configuration.Configuration Configuration { get; set; }
    }
}
