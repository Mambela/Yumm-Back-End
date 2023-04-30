using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._MenuRepository._MenuGroupRepository
{
    public class MenuGroupRepository : IMenuGroupRepository
    {
        ApplicationDbContext _dbContext;
        public MenuGroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AttachProductToMenugroup(MenuGroupsProducts menugroupProducts)
        {

             await _dbContext.MenuGroupsProducts.AddAsync(menugroupProducts);

            return Save();
        }

        public bool CheckMenuGroupExist(int menuGroupId)
        {
            return _dbContext.MenuGroups.Any(mg => mg.Id == menuGroupId);
        }

        public async Task<bool> CreateRestaurantMenuGroup(MenuGroup menuGroup)
        {
            await _dbContext.MenuGroups.AddAsync(menuGroup);


            return Save();
        }

        public bool DeleteMenu(MenuGroup menuGroup)
        {
            _dbContext.Remove(menuGroup);

            return Save();
        }

        public bool DeleteProductToMenugroup(MenuGroupsProducts menuGroupsProducts)
        {

            _dbContext.MenuGroupsProducts.Remove(menuGroupsProducts);

            return Save();
        }

        public ICollection<MenuGroup> GetMenuGroups()
        {

            return _dbContext.MenuGroups.ToList();
        }

        public MenuGroup GetRestaurantMenuGroup(int menuGroupId)
        {

            return _dbContext.MenuGroups.First(m => m.Id == menuGroupId);

        }

        public ICollection<MenuGroup> GetRestaurantMenuGroups(int restaurantId)
        {
            return _dbContext.MenuGroups.Include(m => m.Menu).Include(mg => mg.Menu.MenuGroups).Where(r => r.Menu.RestaurantId == restaurantId).ToList();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateMenu(MenuGroup menuGroup)
        {

            _dbContext.MenuGroups.Update(menuGroup);

            return Save();
        }
    }
}
