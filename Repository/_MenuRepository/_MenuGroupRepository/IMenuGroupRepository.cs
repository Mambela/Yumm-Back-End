using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._MenuRepository._MenuGroupRepository
{
    public interface IMenuGroupRepository
    {
        Task<bool> CreateRestaurantMenuGroup(MenuGroup menuGroup);

        ICollection<MenuGroup> GetMenuGroups();

        ICollection<Restaurant> GetRestaurantMenuGroups(int restaurantId);

        MenuGroup GetRestaurantMenuGroup(int menuGroupId);
        
        Task<bool> AttachProductToMenugroup(MenuGroupsProducts menugroupProducts);

        bool DeleteProductToMenugroup(MenuGroupsProducts menuGroupsProducts);

        bool CheckMenuGroupExist(int menuGroupId);

        bool UpdateMenu(MenuGroup menuGroup);

        bool DeleteMenu(MenuGroup menuGroup);


        bool Save();

    }
}
