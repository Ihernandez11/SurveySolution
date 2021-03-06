namespace SurveySolution.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SurveySolution.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SurveySolution.Models.ApplicationDbContext>
    {
        
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SurveySolution.Models.ApplicationDbContext context)
        {
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

            context.Users.AddOrUpdate(                u => u.UserName,                new Models.ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Izzy",
                    LastName = "H",                }                );

            context.SaveChanges();

            
            // Create a UserManager to add a password to the previously created user
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            //UserManage.AddPassword will also add password encryption
            UserManager.AddPassword(context.Users.Where(u => u.Email == "admin@admin.com").FirstOrDefault().Id, "P@ssword123");

            // Create RoleManager to create role for 'Admin'
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));            RoleManager.Create(new IdentityRole
            {                Name = "Admin"            });

            RoleManager.Create(new IdentityRole
            {
                Name = "Customer"
            });

            context.SaveChanges();

            UserManager.AddToRole(context.Users.Where(u => u.Email == "admin@admin.com").FirstOrDefault().Id.ToString(), "Admin");

            context.SaveChanges();





        }
    }
}
