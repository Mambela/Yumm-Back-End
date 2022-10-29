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

        public bool CheckMenuGroupExist(int menuGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateRestaurantMenuGroup(MenuGroup menuGroup)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMenu(MenuGroup menuGroup)
        {
            throw new NotImplementedException();
        }

        public ICollection<MenuGroup> GetMenuGroups()
        {
            throw new NotImplementedException();
        }

        public MenuGroup GetRestaurantMenuGroup(int menuGroupId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Restaurant> GetRestaurantMenuGroups(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateMenu(MenuGroup menuGroup)
        {
            throw new NotImplementedException();
        }

        //public bool CheckMenuGroupExist(int menuGroupId)
        //{
        //    return _dbContext.MenuGroups.Any(mg => mg.Id == menuGroupId);
        //}

        //public async Task<bool> CreateRestaurantMenuGroup(MenuGroup menuGroup)
        //{
        //    await  _dbContext.MenuGroups.AddAsync(menuGroup);


        //    return Save();
        //}

        //public bool DeleteMenu(MenuGroup menuGroup)
        //{
        //    _dbContext.Remove(menuGroup);

        //    return Save();
        //}

        //public ICollection<MenuGroup> GetMenuGroups()
        //{

        //    return _dbContext.MenuGroups.ToList();
        //}

        //public MenuGroup GetRestaurantMenuGroup(int menuGroupId)
        //{

        //    return _dbContext.MenuGroups.First(m => m.Id == menuGroupId);

        //}

        //public ICollection<Restaurant> GetRestaurantMenuGroups(int restaurantId)
        //{
        //    return _dbContext.Restaurants.Include(m => m.Menu).Include(mg => mg.Menu.MenuGroups).Where(r => r.Id == restaurantId).ToList();
        //}

        //public bool Save()
        //{
        //    return _dbContext.SaveChanges() >= 0;
        //}

        //public bool UpdateMenu(MenuGroup menuGroup)
        //{

        //    _dbContext.MenuGroups.Update(menuGroup);

        //    return Save();
        //}
    }
}
