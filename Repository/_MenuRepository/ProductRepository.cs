using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Data;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Services;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Repository._MenuRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public ProductRepository(ApplicationDbContext dbContext, IWebHostEnvironment webHostenvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostenvironment;
 
        }

        public async Task<bool> CreateItem(Product item)
        {

            //await _dbContext.Products.AddAsync(item);

            return Save();
        }

        public bool DeleteItem(Product item)
        {

            //_dbContext.Products.Remove(item);

            return Save();
        }

       

        public Product GetItem(int itemId)
        {
            return _dbContext.Products.FirstOrDefault(i => i.Id == itemId);
        }

        public ICollection<Product> GetItems()
        {

            return _dbContext.Products.ToList();
        }

        public ICollection<Product> GetItemsInCategory(int categoryId)
        {
            return _dbContext.Products.Include(r => r.category).Where(c => c.categoryId == categoryId).ToList();
        }

        public bool ItemExists(int itemId, int restaurantId)
        {

            return _dbContext.Products.Any(i => i.Id == itemId);
        }

        public bool ItemExists(string name, int restaurantId, int categoryId)
        {

            return _dbContext.Products.Any(i => i.Name == name && i.RestaurantId == restaurantId && i.categoryId == categoryId);
        }

        public bool ItemExists(int itemId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public async Task<MediaResponse> SaveProductImage(CreateProductDTO objectFile)
        {
            //Few Classes required to Save the image correctly;
            
            
            //Class to generate Image uniqueId
                Generators gen = new Generators();
           

            //Class responsible for responding the success of the upload
                MediaResponse mdResponse = new MediaResponse();

            //Class Responsible to validate an Image
            MediaValidator _mediaValid = new MediaValidator();
            
            
            //Validate the image
            mdResponse = _mediaValid.ValidateImage(objectFile.Files.ContentType,objectFile.Files.Length);

            if (!mdResponse.Success)
                return mdResponse;


            Console.WriteLine(objectFile.Files.Length / 1048);
            
            Console.WriteLine(mdResponse.Message);
            
            
            string reference = objectFile.RestaurantId.ToString();
            string resourceRoot = "\\resources\\", _entity = "\\" + "restaurant" + "\\", images = "\\images\\";


            try
            {
                if (objectFile.Files.Length > 0)
                {
                    string _root = _webHostEnvironment.WebRootPath + resourceRoot + _entity + reference  + images;


                    if (!Directory.Exists(_root))
                    {
                        Directory.CreateDirectory(_root);
                    }


                    Console.WriteLine(_root);
                    string uniqueID = gen.GenerateUniqueNumber();
                           
                    
                    string samplePath =(uniqueID.Trim() + objectFile.Files.FileName);
                    samplePath = samplePath.Trim();
                    
                    

                    using (FileStream fileStream = System.IO.File.Create(_root + samplePath))
                    {
                        await objectFile.Files.CopyToAsync(fileStream);
                        fileStream.Flush();

                        mdResponse.Message = samplePath;

                        return mdResponse;
                    }

                }

                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UpdateItem(Product item)
        {

            _dbContext.Products.Update(item);

            return Save();
        }

        public Task<MediaResponse> DeleteProductImage(UpdateProductDTO objectFile, string previousImage)
        {
            throw new NotImplementedException();
        }
    }
}
