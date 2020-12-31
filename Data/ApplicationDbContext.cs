using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yumm_Back_End.Models;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        #region Restaurant Entities
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Item>()
                .Property(I => I.Price)
                .HasPrecision(18, 2);

           
            

           // SeedUser(builder);

            base.OnModelCreating(builder);
        }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Working_Hours> Working_Hours { get; set; }

        public DbSet<RestaurantType> RestaurantTypes { get; set; }

        #endregion


        #region Address Entities

        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Commune> Communes { get; set; }

        public DbSet<Neighbourhood> Neighbourhoods { get; set; }

        public DbSet<Street> Streets { get; set; }
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

            PasswordHasher<ApplicationUser> appUser = new PasswordHasher<ApplicationUser>();

            superUser.PasswordHash = appUser.HashPassword(superUser, "4kingmambela?");

            //seed user

            builder.Entity<ApplicationUser>().HasData(superUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = SUPER_ADMIN_ID
            });
        }



        #region Client Entities

        //public DbSet<Client> Clients { get; set; }
        #endregion


        #region Additional Database Modification

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            onBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void onBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                //for entities that inherit from BaseEntity
                //set UpdateDate/ CreatedDate appropriately
                //

                if (entry.Entity is BaseEntity trackable)
                { 
                    switch(entry.State)
                    {
                        case EntityState.Modified:
                            //set the updated date to now
                            trackable.UpdatedDate = utcNow;

                            //mark property as dont update
                            entry.Property("CreateDate").IsModified = false;
                            break;

                        case EntityState.Added:
                            
                            //set both updated and created date to now
                            trackable.CreateDate = utcNow;
                            trackable.UpdatedDate = utcNow;

                            break;
                    }
                }


            }
 
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            onBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                 cancellationToken));
        }

        #endregion
    }
}
