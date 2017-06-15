using System.Threading.Tasks;
using CallCenter.API.DomainModel.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CallCenter.API.Repository.Interfaces.Membership
{
    public interface IAuthorizationRepository
    {
        //Task<IdentityResult> RegisterUser(UserModel userModel);
        Task<ApplicationUser> FindUser(string userName, string password);
    }
}
