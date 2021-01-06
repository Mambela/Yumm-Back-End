using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Repository._MediaRepository;

namespace Yumm_Back_End.Controllers.Media_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediasController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MediasController(IWebHostEnvironment webHostenvironment)
        {
            _webHostEnvironment = webHostenvironment;
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

    }
}
