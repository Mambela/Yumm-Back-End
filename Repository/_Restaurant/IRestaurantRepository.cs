using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._Restaurant
{
    public interface IRestaurantRepository
    {
        //Check if a username of the restaurant exists

        ICollection<Restaurant> GetRestaurants();

        ICollection<Restaurant> GetRestaurantsInType(int typeId);
        Restaurant GetRestaurant(int restaurantI);

        bool RestaurantExist(string name);

        bool RestaurantExist(int restaurantId);

       Task <bool> CreateRestaurant(Restaurant restaurant);

        bool UpdateResturant(Restaurant restaurant);

        bool DeleteRestaurant(Restaurant restaurant);


        bool Save();

    }
}
