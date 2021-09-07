using Microsoft.EntityFrameworkCore;
using SocialBrothersAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialBrothersAPIDemo.Database
{
    public class DataContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = SocialBrothersAPIDemo.db");
        }

        public DbSet<Adres> Adressen { get; set; }
    }
}
