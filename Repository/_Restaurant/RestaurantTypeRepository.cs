using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._Restaurant
{
    public class RestaurantTypeRepository : IRestaurantTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RestaurantTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public int Count()
        {
            return _dbContext.RestaurantTypes.Count();
        }

        public async Task<bool> CreateType(RestaurantType type)
        {

            await _dbContext.RestaurantTypes.AddAsync(type);

            return Save();
        }

        public bool DeleteType(int restaurantId)
        {
            var type = GetRestaurantType(restaurantId);

            _dbContext.RestaurantTypes.Remove(type);

            return Save();
        }

        public ICollection<RestaurantType> GetRestaurantsInTypes(int id)
        {

            return _dbContext.RestaurantTypes.Include(rt => rt.Restaurant).Where(r => r.Id == id).ToList();

        }

        public RestaurantType GetRestaurantType(int typeId)
        {
            return _dbContext.RestaurantTypes.FirstOrDefault(t => t.Id == typeId);
        }

        public ICollection<RestaurantType> GetRestaurantTypes(PaginationFilter validFilter)
        {

            return _dbContext.RestaurantTypes
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .OrderBy(c =>c.Name)
                .ToList();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool TypeExists(string name)
        {

            return _dbContext.RestaurantTypes.Any(rt => rt.Name == name);
        }

        public bool TypeExists(int typeId)
        {
            return _dbContext.RestaurantTypes.Any(rt => rt.Id == typeId);
        }

        public bool UpdateType(RestaurantType type)
        {
            _dbContext.RestaurantTypes.Update(type);

            return Save();

        }
    }
}
