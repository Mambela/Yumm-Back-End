using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._ProductCategory
{
    public interface ICategoryRepository
    {

        ICollection<Category> GetCategories();

        Category GetCategory(int categoryId);

        bool CategoryExists(int categoryId);

        bool CategoryExists(string category);

        Task<bool> CreateCategory(Category category);

        bool UpdateCategory(Category category);

        bool DeleteCategory(Category category);

        bool Save();


    }
}
