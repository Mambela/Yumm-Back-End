using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._MenuRepository._ProductRepository
{
    public class ProductExtrasRepository : IProductExtrasRepository
    {
        private readonly ApplicationDbContext _dbContext;


        public ProductExtrasRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProductExtras(ProductExtras productExtras)
        {

            await _dbContext.ProductsExtras.AddAsync(productExtras);

            return Save();
        }

        public bool DeleteProductExtra(ProductExtras productExtras)
        {
            _dbContext.ProductsExtras.Remove(productExtras);

            return Save();
        }

        public ProductExtras GetProductExtra(int productExtraId)
        {
            return _dbContext.ProductsExtras.FirstOrDefault(p => p.Id == productExtraId);
        }

        public ICollection<ProductExtras> GetProductExtras()
        {
            return _dbContext.ProductsExtras.ToList();
        }

        public ICollection<ProductExtras> GetProductExtras(int productId)
        {
            return _dbContext.ProductsExtras.Where(e => e.ProductId == productId).ToList();
        }

        public bool UpdateProductExtra(ProductExtras productExtras)
        {

            _dbContext.ProductsExtras.Update(productExtras);

            return Save();

        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool ProductExtrasExist(int productExtraId)
        {
            return _dbContext.ProductsExtras.Any(p => p.Id == productExtraId);
        }

        public bool ProductExtrasExist(string productExtra)
        {
            return _dbContext.ProductsExtras.Any(p => p.Name.Trim().ToLower() == productExtra.Trim().ToLower());
        }
    }
}
