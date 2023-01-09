using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.DTOs.PaginationDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Services;
using Yumm_Back_End.Services.Logger;
using Yumm_Back_End.Services.MediaService;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Repository._MenuRepository._ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILoggerService _loggerService;
        private readonly IMediaManager _mediaManager;

        public ProductRepository(ApplicationDbContext dbContext, IWebHostEnvironment webHostenvironment, ILoggerService loggerService, IMediaManager mediaManager)
        {
            _loggerService = loggerService;
            _dbContext = dbContext;
            _webHostEnvironment = webHostenvironment;
            _mediaManager = mediaManager;   
        }


        public async Task<bool> CreateProduct(Product item)
        {
            await _dbContext.Products.AddAsync(item);

            return Save();
        }

        public async Task<bool> CreateProductSize(List<ProductSize> productSizes)
        {
            
            await _dbContext.ProductSizes.AddRangeAsync(productSizes);
            
            return Save();
        }

        public bool DeleteProduct(Product item)
        {
            _dbContext.Products.Remove(item);

            return Save();
        }

        public Task<MediaResponse> DeleteProductImage(UpdateProductDTO objectfile, string previousimage)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int ProductId)
        {
            return _dbContext.Products
                .Include(s => s.ProductSizes)
                .FirstOrDefault(i => i.Id == ProductId);
        }

        public ICollection<Product> GetProducts()
        {
            return _dbContext.Products
                .Include(s => s.ProductSizes)
                .ToList();
        }

        public ICollection<Product> GetProductsInCategory(int categoryId)
        {
            return _dbContext.Products.Include(s =>s.ProductSizes)
                .Include(r => r.category).Where(c => c.categoryId == categoryId).ToList();
        }

        public ICollection<Product> GetProductsInCategory(PaginationFilter pagination, int categoryId)
        {
            return _dbContext.Products.Include(s => s.ProductSizes)
                  .Include(r => r.category)
                  .Skip((pagination.PageNumber-1)* pagination.PageSize)
                  .Take(pagination.PageSize)
                  .Where(c => c.categoryId == categoryId).ToList();
        }

        public ICollection<Product> GetProductsInMenuGroup(int menuGroupId)
        {

            return _dbContext.Products
                .Include(s => s.ProductSizes)
                .Include(c =>c.category)
                .Include(m =>m.MenuGroup)
                .Where(mg => mg.MenuGroupId == menuGroupId).ToList();
        }

        public ICollection<Product> GetProductsInMenuGroup(PaginationFilter pagination, int menuGroupId)
        {
            
            return _dbContext.Products
              .Include(s => s.ProductSizes)
              .Include(c => c.category)
              .Include(m => m.MenuGroup)
              .Skip((pagination.PageNumber-1)* pagination.PageSize)
              .Take(pagination.PageSize)
              .Where(mg => mg.MenuGroupId == menuGroupId).ToList();

        }

        public ICollection<Product> GetProductsInRestaurant(int restaurantId)
        {
            return _dbContext.Products
                .Include(s => s.ProductSizes)
                .Include(c => c.category)
                .Include(m => m.MenuGroup)
                .Where(p =>p.MenuGroup.Menu.RestaurantId == restaurantId).ToList();
        }

        public ICollection<Product> GetProductsInRestaurant(PaginationFilter pagination, int restaurantId)
        {
            return _dbContext.Products
               .Include(s => s.ProductSizes)
               .Include(c => c.category)
               .Include(m => m.MenuGroup)
               .Skip((pagination.PageNumber-1)* pagination.PageSize)
               .Take(pagination.PageSize)
               .Where(p => p.MenuGroup.Menu.RestaurantId == restaurantId).ToList();
        }

        public bool ProductExists(int itemId)
        {
            return _dbContext.Products.Any(i => i.Id == itemId);
        }

        public bool ProductExists(string name, int restaurantId, int categoryId)
        {
            return _dbContext.Products.Any(i => i.Name == name && i.categoryId == categoryId);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public async Task<MediaResponse> SaveProductImage(CreateProductDTO objectFile,int referenceId,string _entity )
        {
            //Few Classes required to Save the image correctly;


            //Class to generate Image uniqueId
            Generators gen = new Generators();


            //Class responsible for responding the success of the upload
            MediaResponse mdResponse = new MediaResponse();

            //Class Responsible to validate an Image
            


            //Validate the image
            mdResponse = _mediaManager.ValidateImage(objectFile.Files.ContentType, objectFile.Files.Length);

            if (!mdResponse.Success)
                return mdResponse;


            string reference = referenceId.ToString();
            string resourceRoot = "\\resources\\", images = "\\images\\";


            try
            {
                if (objectFile.Files.Length > 0)
                {
                    string _root = _webHostEnvironment.WebRootPath + resourceRoot + _entity + reference + images;


                    if (!Directory.Exists(_root))
                    {
                        Directory.CreateDirectory(_root);
                    }


                    Console.WriteLine(_root);
                    string uniqueID = gen.GenerateUniqueNumber();


                    string samplePath = (uniqueID.Trim() + objectFile.Files.FileName);
                    samplePath = samplePath.Trim();

                    


                    using (FileStream fileStream = System.IO.File.Create(_root + samplePath))
                    {
                        await objectFile.Files.CopyToAsync(fileStream);
                        fileStream.Flush();

                        mdResponse.imagePath = samplePath;

                        return mdResponse;
                    }

                }

                return null;
            }
            catch (Exception ex)
            {

                _loggerService.LogError("Error image " +ex.Message);

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UpdateProduct(Product item)
        {

            _dbContext.Products.Update(item);

            return Save();
        }
    }
}
