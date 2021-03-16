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
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.Tickets;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Product>()
                .Property(I => I.Price)
                .HasPrecision(18, 2);

            builder.Entity<ProductExtras>()
                   .Property(I => I.Price)
                   .HasPrecision(18, 2);

            builder.Entity<Order>()
                    .Property(P => P.Amount)
                    .HasPrecision(18, 2);


            //Locations Builder
            builder.Entity<Location>().Property(L => L.Lat).HasPrecision(8,6);
            builder.Entity<Location>().Property(L => L.Lng).HasPrecision(9,6);

            builder.Entity<UsersLocations>().Property(L => L.Lat).HasPrecision(8, 6);
            builder.Entity<UsersLocations>().Property(L => L.Lng).HasPrecision(9, 6);


            //Associating Locations To user
            builder.Entity<UsersLocations>().HasKey(x => new {x.UserId,x.LocationId});

            //builder.Entity<OrdersProducts>().HasKey(x => new { x.OrderId, x.ProductId });



            // SeedUser(builder);

            base.OnModelCreating(builder);
        }
       
        #region Restaurant Entities

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }


        public DbSet<ProductExtras> ProductsExtras { get; set; }

        public DbSet<Working_Hours> Working_Hours { get; set; }

        public DbSet<RestaurantType> RestaurantTypes { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuGroup> MenuGroups { get; set; }


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

        #region Orders

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<OrdersProducts> OrdersProducts { get; set; }

        public DbSet<OrderProductExtras> OrderProductExtras { get; set; }
        #endregion


        #region Client Entities

        public DbSet<Client> Clients { get; set; }
        #endregion


        #region Additional Database Mods

           // Entities added for created date and Update date. 

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

        #region Tickets 
      //  public DbSet<Ticket_Type> Ticket_Types { get; set; }
        #endregion
    }
}
