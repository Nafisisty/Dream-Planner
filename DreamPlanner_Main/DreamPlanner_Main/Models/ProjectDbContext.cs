using DreamPlanner_Main.Models.UserDefinedModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DreamPlanner_Main.Models
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<PartyType> PartyTypes { get; set; }

        public DbSet<Theme> Themes { get; set; }

    }
}