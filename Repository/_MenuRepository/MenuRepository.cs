using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._MenuRepository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MenuRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<bool> CreateRestaurantMenu(Menu Menu)
        {

            await _dbContext.Menus.AddAsync(Menu);

            return Save();

        }

        public bool DeleteMenu(Menu menu)
        {
            _dbContext.Menus.Remove(menu);

            return Save();
        }

        public ICollection<Menu> GetMenus()
        {
            return _dbContext.Menus.Include(m => m.Restaurant).ToList();
        }

        public Menu GetMenu(int MenuId)
        {
            return _dbContext.Menus.Include(m => m.Restaurant).First(m => m.Id == MenuId);
        }

        public ICollection<Menu> GetRestaurantMenus(int restaurantId)
        {
            return _dbContext.Menus.Include(r => r.Restaurant).Where(r => r.RestaurantId == restaurantId).ToList();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateMenu(Menu menu)
        {

            _dbContext.Menus.Update(menu);

            return Save();
        }

        public bool MenuExists(int menuId)
        {
            return _dbContext.Menus.Any(m => m.Id == menuId);
        }
    }
}
