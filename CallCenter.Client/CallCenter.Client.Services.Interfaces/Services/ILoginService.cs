using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Models;

namespace CallCenter.Client.Services.Interfaces.Services
{
    public interface ILoginService
    {
        Task<UserModel> Login(string username, string password);
    }
}
