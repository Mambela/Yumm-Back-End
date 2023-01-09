using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
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


        public async Task<bool> CreateRestaurant(Restaurant restaurant)
        {

            await _dbContext.Restaurants.AddAsync(restaurant);

            return Save();
        }

        public bool DeleteRestaurant(Restaurant restaurant)
        {
            _dbContext.Restaurants.Remove(restaurant);

            return Save();

        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            return _dbContext.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
        }

        public ICollection<Restaurant> GetRestaurants()
        {
            return _dbContext.Restaurants.ToList();
        }


        public bool UpdateResturant(Restaurant restaurant)
        {

            _dbContext.Restaurants.Update(restaurant);

            return Save();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool RestaurantExist(string name)
        {
            return _dbContext.Restaurants.Any(r => r.Name == name);
        }

        public bool RestaurantExist(int restaurantId)
        {
            return _dbContext.Restaurants.Any(r => r.Id == restaurantId);
        }

        public ICollection<Restaurant> GetRestaurantsInType(int typeId)
        {
            return _dbContext.Restaurants.Include(rt => rt.RestaurantType).Where(rt => rt.RestaurantTypeId == typeId).ToList();
        }

        public List<Restaurant> GetRestaurantsPaged(PaginationFilter validFilter)
        {
           return _dbContext.Restaurants
                .Include(rt =>rt.RestaurantType)
                .Include(u =>u.User)
                .Skip((validFilter.PageNumber -1)* validFilter.PageSize)
                .Take(validFilter.PageSize).ToList(); 
        }

        public List<Restaurant> GetRestaurantsPagedByType(PaginationFilter validFilter , int typeId)
        {
            return _dbContext.Restaurants.Include(rt => rt.RestaurantType)
                .Include(u => u.User)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .Where(rt => rt.RestaurantTypeId == typeId).ToList();
        }

        public List<Restaurant> GetRestaurantsByReference(PaginationFilter validFilter,string reference)
        {
            return _dbContext.Restaurants
                .Include(rt => rt.RestaurantType)
                .Include(u => u.User)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                 .Where(r => r.Reference.ToLower().Contains(reference.ToLower())).ToList();
        }

        public int Count()
        {
            return _dbContext.Restaurants.Count();
        }
    }
}
