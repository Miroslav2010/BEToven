using BETOven.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BETOven.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BETOven.Models.BetovenDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed (BetovenDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
           
                if (RoleManager.RoleExists("Admin") == false)
                {
                    RoleManager.Create(new IdentityRole("Admin"));
                }

                if (RoleManager.RoleExists("User") == false)
                {
                    RoleManager.Create(new IdentityRole("User"));
                }

        }
    }
}
