using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;
using Yumm_Back_End.Models.DTOs.RestaurantDTO;
using Yumm_Back_End.Repository._MediaRepository;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;

namespace Yumm_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MediaRepository _mediaRepository;

     
        public class FileUpload
        {
            public IFormFile files { get; set; }
        }

        public UploadController(IWebHostEnvironment webHostenvironment, MediaRepository mediaRepository)
        {
            _webHostEnvironment = webHostenvironment;
            _mediaRepository = mediaRepository;
        }

        [HttpPost]
        public async Task<string> PostAsync([FromForm] CreateProductDTO objectFile)
        {

            var path = await _mediaRepository.SaveFoodImage(objectFile);


            #region old code
            //string uploads = "\\uploads\\",restaurant ="\\restaurants\\",foodItems="\\food_items\\",reference ="\\34\\";


            //try
            // {
            //    if (objectFile.Files.Length > 0)
            //    {
            //        string path = _webHostEnvironment.WebRootPath +  uploads + restaurant +reference +foodItems;
            //        DateTime uniqueDate = DateTime.UtcNow;

            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }
            //        string filePath = path;


            //        Console.WriteLine(filePath);

            //        string samplePath = objectFile.Files.FileName.Trim().ToLower();

            //        Console.WriteLine(samplePath);


            //        using (FileStream fileStream = System.IO.File.Create(path + samplePath))
            //        {

            //            objectFile.Files.CopyTo(fileStream);
            //            Console.WriteLine(fileStream);
            //            fileStream.Flush();
            //            return "File Uploaded successfully";
            //        }

            //    }
            //    else
            //    {
            //        return "Error occured during file updaload";
            //    }


            //}
            //catch (Exception ex)
            //{
            //    return ex.Message.ToString();
            //}
            #endregion

            return "";
        }

        [HttpGet]
        public IActionResult Get(string name)
        {
            var path = "wwwroot\\uploads\\restaurants\\34\\food_items\\" + name;

            Byte[] b = System.IO.File.ReadAllBytes(path);

            return File(b, "image/jpeg");

        }
      

        
    }
}
