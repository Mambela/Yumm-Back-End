using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Repository._Restaurant;

namespace Yumm_Back_End.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public RestaurantRepository(ApplicationDbContext context)
        {
            _dbContext = context;
                
        }


        public bool CreateRestaurant(Restaurant restaurant)
        {

            _dbContext.Restaurants.Add(restaurant);
            
            //throw new NotImplementedException();

            return Save();
        }

        public bool DeleteRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public Restaurant GetRestaurant(int restaurantI)
        {
            throw new NotImplementedException();
        }

        public ICollection<Restaurant> GetRestaurants()
        {
            throw new NotImplementedException();
        }

        public bool IsUniqueEmail(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public bool IsUniquePhone(int phoneNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }


        public bool UpdateResturant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
