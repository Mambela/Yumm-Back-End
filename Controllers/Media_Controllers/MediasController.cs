using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Repository._MediaRepository;
using Yumm_Back_End.Services.Logger;

namespace Yumm_Back_End.Controllers.Media_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediasController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILoggerService loggerSerive;
        public MediasController(IWebHostEnvironment webHostenvironment,ILoggerService _loggerService) 
        {
            _webHostEnvironment = webHostenvironment;
            loggerSerive = _loggerService;
        }
        

        [HttpGet]
        public IActionResult GetProductImage(string name,int reference)
        {
            string resourceRoot = "\\resources\\", _entity = "\\" + "restaurant" + "\\", images = "\\images\\";
            string _root = _webHostEnvironment.WebRootPath + resourceRoot + _entity + reference + images;
            var path = _root + name;

            Byte[] b = System.IO.File.ReadAllBytes(path);

            return File(b, "image/jpeg");

        }

        [HttpGet("DriverImage")]

        public IActionResult DriverImage(string name)
        {
            try
            {

                string resourceRoot = "\\resources\\", _entity = "\\" + "driver" + "\\",reference="\\profilepicture\\" ,images = "\\images\\";
                string _root = _webHostEnvironment.WebRootPath + resourceRoot + _entity + reference + images;
                var path = _root + name;

                Byte[] b = System.IO.File.ReadAllBytes(path);

                return File(b, "image/jpeg");

               
            }
            catch (Exception ex)
            {
                loggerSerive.LogError("error" + ex.Message);

                ModelState.AddModelError("Error", "Failed to retrieve driver Image");
                return BadRequest(ModelState);
            }
        }

    }
}
