using DreamPlanner_Main.Models.UserDefinedModels;
using DreamPlanner_Main.Models.UserDefineModels;
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

        public DbSet<Contact> Contact { get; set; }
    }
}