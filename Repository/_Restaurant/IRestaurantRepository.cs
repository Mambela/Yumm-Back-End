using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._Restaurant
{
    public interface IRestaurantRepository
    {
        //Check if a username of the restaurant exists

        int Count();

        ICollection<Restaurant> GetRestaurants();

        ICollection<Restaurant> GetRestaurantsInType(int typeId);
        Restaurant GetRestaurant(int restaurantI);
        List<Restaurant> GetRestaurantsByReference(PaginationFilter validFilter,string reference);

        bool RestaurantExist(string name);

        bool RestaurantExist(int restaurantId);

       Task <bool> CreateRestaurant(Restaurant restaurant);

        List<Restaurant> GetRestaurantsPaged(PaginationFilter validFilter);

        List<Restaurant> GetRestaurantsPagedByType(PaginationFilter validFilter, int typeId);

        bool UpdateResturant(Restaurant restaurant);

        bool DeleteRestaurant(Restaurant restaurant);


        bool Save();

    }
}
