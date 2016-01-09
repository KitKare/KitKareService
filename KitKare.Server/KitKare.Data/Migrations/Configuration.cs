namespace KitKare.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<KitKare.Data.KitKareDbContext>
    {
        private UserManager<User> userManager;
        private List<string> seededUsersIds = new List<string>();

        private byte[] defaultImageData;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(KitKareDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));

            this.SeedRoles(context);
            this.SeedUsers(context);
            
        }

        private void SeedRoles(KitKareDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Admin"));
            context.SaveChanges();
        }

        private void SeedUsers(KitKareDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            this.SeedAdmin(context);
            this.SeedRegularUsers(context);
        }

        private void SeedAdmin(KitKareDbContext context)
        {
            var admin = new User
            {
                Email = "admin@admin.com",
                UserName = "admin"
            };

            this.userManager.Create(admin, "admin11");
            this.userManager.AddToRole(admin.Id, "Admin");

            context.SaveChanges();
        }

        private void SeedRegularUsers(KitKareDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Email = "user@user.com",
                    UserName = "user" + i
                };

                this.userManager.Create(user, "user11");
                seededUsersIds.Add(user.Id);
            }

            context.SaveChanges();
        }
    }
}
