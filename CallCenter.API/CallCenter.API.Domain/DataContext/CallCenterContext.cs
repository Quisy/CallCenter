using System.Data.Entity;
using CallCenter.API.DomainModel.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CallCenter.API.Domain.DataContext
{
    public class CallCenterContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        public CallCenterContext() : base("CallCenterContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}
