using CallCenter.API.DomainModel.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CallCenter.API.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CallCenter.API.Domain.DataContext.CallCenterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CallCenter.API.Domain.DataContext.CallCenterContext context)
        {
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            //var customerRole = roleManager.FindByName("Customer");
            //if (customerRole == null)
            //{
            //    customerRole = new ApplicationRole("Customer");
            //    roleManager.Create(customerRole);
            //}

            //var employeeRole = roleManager.FindByName("Employee");
            //if (employeeRole == null)
            //{
            //    employeeRole = new ApplicationRole("Employee");
            //    roleManager.Create(employeeRole);
            //}

            //var customer1 = new ApplicationUser { UserName = "customer1" };
            //userManager.Create(customer1, "customer1");

            //var customer2 = new ApplicationUser { UserName = "customer2" };
            //userManager.Create(customer2, "customer2");

            //var employee1 = new ApplicationUser { UserName = "employee1" };
            //userManager.Create(employee1, "employee1");

            //var employee2 = new ApplicationUser { UserName = "employee2" };
            //userManager.Create(employee2, "employee2");


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
