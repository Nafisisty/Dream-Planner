namespace DreamPlanner_Main.Migrations
{
    using Models;
    using Models.UserDefinedModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectDbContext context)
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

            context.Halls.AddOrUpdate(
                new Hall { HallName = "Small (50 persons)", HallCapacity = 50 },
                new Hall { HallName = "Medium (100 persons)", HallCapacity = 100 },
                new Hall { HallName = "Large (200 persons)", HallCapacity = 200 }
                );

            context.PartyTypes.AddOrUpdate(
                new PartyType { PartyTypeName = "Wedding" },
                new PartyType { PartyTypeName = "Birthday" },
                new PartyType { PartyTypeName = "Muslim Community" },
                new PartyType { PartyTypeName = "Hindu Community" },
                new PartyType { PartyTypeName = "Buddhist Community" },
                new PartyType { PartyTypeName = "Christian Community" }
                );

            context.Ratings.AddOrUpdate(
                new Rating { RatingName = "Very Bad"},
                new Rating { RatingName = "More should be improved" },
                new Rating { RatingName = "Satisfied" },
                new Rating { RatingName = "Awesome" }

                );
        }
    }
}
