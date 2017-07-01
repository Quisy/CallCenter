using Microsoft.AspNet.Identity.EntityFramework;

namespace CallCenter.API.DomainModel.DomainModels
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            
        }

        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}
