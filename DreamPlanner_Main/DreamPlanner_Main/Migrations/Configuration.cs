namespace DreamPlanner_Main.Migrations
{
    using Models.UserDefinedModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DreamPlanner_Main.Models.ProjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DreamPlanner_Main.Models.ProjectDbContext context)
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

            context.Countries.AddOrUpdate(
                c => c.CountryName,
                new Country { CountryName = "Bangladesh" },
                new Country { CountryName = "China" },
                new Country { CountryName = "India"},
                new Country { CountryName = "Pakistan"}
                );
        }
    }
}
