using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._FoodCategory
{
    public class CategoryRepository : ICategoryRepository
    {

        ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public bool CategoryExists(int categoryId)
        {
            return _dbContext.Categories.Any(c => c.Id == categoryId);
        }

        public bool CategoryExists(string categoryName)
        {
            return _dbContext.Categories.Any(c => c.Name == categoryName);
        }

        public async Task<bool> CreateCategory(Category category)
        {

             await _dbContext.Categories.AddAsync(category);

            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _dbContext.Categories.Remove(category);

            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {

            return _dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateCategory(Category category)
        {

            _dbContext.Categories.Update(category);

            return Save();
            
        }
    }
}
