using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Services;
using Yumm_Back_End.Services.MediaService;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Repository._MediaRepository
{
    public class MediaRepository : IMediaRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MediaResponse _mediaResponse;
        private readonly MediaManager _mediaValidator;
        public MediaRepository(IWebHostEnvironment webHostenvironment,MediaResponse mediaResponse, MediaManager mediaValidator)
        {
            _webHostEnvironment = webHostenvironment;
            _mediaResponse = mediaResponse;
            _mediaValidator = mediaValidator;
        }
        public async Task<bool> SaveFoodImage(CreateProductDTO objectFile, string entity)
        {

            Console.WriteLine(objectFile.Files.Length);
            Console.WriteLine(objectFile.Files.GetType());

            return false;


            string reference = objectFile.RestaurantId.ToString();

            string resourceRoot = "\\resources\\", _entity = "\\" + entity + "\\", images = "\\images\\";


            try
            {
                if (objectFile.Files.Length > 0)
                {
                    string _root = _webHostEnvironment.WebRootPath + resourceRoot + reference + _entity + images;


                    if (!Directory.Exists(_root))
                    {
                        Directory.CreateDirectory(_root);
                    }




                    Console.WriteLine(_root);

                    string samplePath = objectFile.Files.FileName.Trim().ToLower();

                    Console.WriteLine(samplePath);

                    using (FileStream fileStream = System.IO.File.Create(_root + samplePath))
                    {
                        await objectFile.Files.CopyToAsync(fileStream);
                        fileStream.Flush();

                        return true;
                    }

                }

                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
                throw;
            }
        }

        public Task<bool> SaveFoodImage(CreateProductDTO objectFile)
        {
            throw new NotImplementedException();
        }
         
        
    }
}
