using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        #region Restaurant Entities
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Item>()
                .Property(I => I.Price)
                .HasPrecision(18, 2);
            
            base.OnModelCreating(builder);
        }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }

      

        #endregion
    }
}
