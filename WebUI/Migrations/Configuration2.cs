using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace WebUI.Migrations
{
    public class Configuration2 : DbMigrationsConfiguration<Domain.Concrete.EFDbContext>
    {
        public Configuration2()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Domain.Concrete.EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }


    }
}