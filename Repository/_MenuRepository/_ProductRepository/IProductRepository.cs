using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Repository._MenuRepository._ProductRepository
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts(PaginationFilter pagination);

        ICollection<Product> GetProductsInCategory(int catgoryId);
        ICollection<Product> GetProductsInCategory(PaginationFilter pagination,int catgoryId);

        ICollection<Product> GetProductsInRestaurant (int restaurantId);
        ICollection<Product> GetProductsInRestaurant (PaginationFilter pagination,int restaurantId);
        ICollection<Product> GetProductsInaRestaurantbyCategory (PaginationFilter pagination,int restaurantId, int categoryId);


        ICollection<Product> GetProductsInMenuGroup(int menuGroupId);
        ICollection<Product> GetProductsInMenuGroup(PaginationFilter pagination,int menuGroupId);


        int CountProductsInaRestaurantbyCategory(int restaurantId, int categoryId);
        int CountProductsInMenuGroup(int menuGroupId);

        int CountProductsInRestaurant(int restaurantId);

        int CountAllProducts();

        int CountProdcutsInCategory(int categoryId);

        Product GetProduct(int productId);

        bool ProductExists(int productId);

        bool ProductExists(string name, int restaurantId, int categoryId);

        Task<bool> CreateProduct(Product product);
        Task<bool> CreateProductSize(List<ProductSize> productSizes);

        List<ProductSize> GetAllProductSizes(int productId);

        bool RemoveProductSize(List<ProductSize> productSizes);

        bool UpdateProduct(Product product);

        bool DeleteProduct(Product product);

        bool Save();

        Task<MediaResponse> SaveProductImage(CreateProductDTO objectfile, int referenceId, string _entity);

        Task<MediaResponse> DeleteProductImage(UpdateProductDTO objectfile, string previousimage);

    }
}
