using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.Services.Interfaces.Base
{
    public interface IApiService : IBaseService
    {
        string GetBasicAuthorizationHeaderValue();
    }
}
