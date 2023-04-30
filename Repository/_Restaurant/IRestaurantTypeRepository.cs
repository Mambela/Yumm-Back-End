using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._Restaurant
{
    public interface IRestaurantTypeRepository
    {

        Task<bool> CreateType(RestaurantType type);

        bool UpdateType(RestaurantType type);

        RestaurantType GetRestaurantType(int typeId);

        ICollection<RestaurantType> GetRestaurantTypes(PaginationFilter validFilter);

        ICollection<RestaurantType> GetRestaurantsInTypes(int id);

        bool DeleteType(int restaurantId);

        int Count();

        bool TypeExists(string name);

        bool TypeExists(int typeId);

        bool Save();
    }
}
