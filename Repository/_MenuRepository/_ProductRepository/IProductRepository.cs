using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Repository._MenuRepository._ProductRepository
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        ICollection<Product> GetProductsInCategory(int catgoryId);

        ICollection<Product> GetProductsInRestaurant (int restaurantId);


        ICollection<Product> GetProductsInMenuGroup(int menuGroupId);

        Product GetProduct(int productId);

        bool ProductExists(int productId);

        bool ProductExists(string name, int restaurantId, int categoryId);

        Task<bool> CreateProduct(Product product);

        bool UpdateProduct(Product product);

        bool DeleteProduct(Product product);

        bool Save();

        Task<MediaResponse> SaveProductImage(CreateProductDTO objectfile);

        Task<MediaResponse> DeleteProductImage(UpdateProductDTO objectfile, string previousimage);

    }
}
