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
        private readonly IMediaRepository _mediaRepository;
        public MediasController(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }



    }
}
