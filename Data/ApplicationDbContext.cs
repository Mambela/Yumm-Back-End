using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Clients;
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

        public DbSet<Working_Hours> Working_Hours { get; set; }

        #endregion


        #region Address Entities

        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Commune> Communes { get; set; }

        public DbSet<Neighbourhood> Neighbourhoods { get; set; }

        public DbSet<Street> Streets { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<UsersLocations> UsersLocations { get; set; }
        #endregion


        //Seed SUPER ADMIN

        private void SeedUser(ModelBuilder builder)
        { 

            string SUPER_ADMIN_ID =  "02174cf0–9412–4cfe - afbf - 59f706d72cf6";

            string ROLE_ID = "341743f0-asd2–42de-afbf-59kkidelejk72cf6";

            builder.Entity<IdentityRole>().HasData(new IdentityRole { 
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //Add user 
            var superUser = new ApplicationUser
            {
                Id = SUPER_ADMIN_ID,
                Email = "kevinDeveloper@yumm.com",
                EmailConfirmed = true,
                UserName = "kevinDeveloper@yumm.com"

            };

        #region Client Entities

        //public DbSet<Client> Clients { get; set; }
        #endregion

    }
}
