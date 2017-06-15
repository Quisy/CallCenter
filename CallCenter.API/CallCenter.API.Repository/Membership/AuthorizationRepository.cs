using System;
using System.Threading.Tasks;
using CallCenter.API.Domain.DataContext;
using CallCenter.API.DomainModel.DomainModels;
using CallCenter.API.Repository.Interfaces.Membership;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CallCenter.API.Repository.Membership
{
    public class AuthorizationRepository : IAuthorizationRepository, IDisposable
    {
        private CallCenterContext _ctx;

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizationRepository()
        {
            _ctx = new CallCenterContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
