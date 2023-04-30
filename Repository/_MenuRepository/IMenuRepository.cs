using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._MenuRepository
{
    public interface IMenuRepository
    {

        Task<bool> CreateRestaurantMenu(Menu Menu);

        ICollection<Menu> GetMenus();

        ICollection<Menu> GetRestaurantMenus(int restaurantId);

        Menu GetMenu(int MenuId);

        bool MenuExists(int menuId);


        bool UpdateMenu(Menu menu);

        bool DeleteMenu(Menu menu);


        bool Save();
    }
}
