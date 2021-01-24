using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Repository._MenuRepository._ProductRepository
{
    public interface IProductExtrasRepository
    {

        Task<bool> CreateProductExtras(ProductExtras productExtras);


        bool ProductExtrasExist(int productExtraId);

        bool ProductExtrasExist(string productExtra);

        ICollection<ProductExtras> GetProductExtras();

        ICollection<ProductExtras> GetProductExtras(int productId);

        ProductExtras GetProductExtra(int productExtraId);

        bool DeleteProductExtra(ProductExtras productExtras);

        bool UpdateProductExtra(ProductExtras productExtras);

        bool Save();

    }
}
