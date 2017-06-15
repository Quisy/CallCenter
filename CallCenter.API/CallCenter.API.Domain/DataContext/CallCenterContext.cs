using System.Data.Entity;
using CallCenter.API.DomainModel.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CallCenter.API.Domain.DataContext
{
    public class CallCenterContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public CallCenterContext() : base("CallCenterContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}
