using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Services.Response;
using Microsoft.AspNetCore.Http;

namespace Yumm_Back_End.Services.MediaService
{
    public interface IMediaManager
    {
        public MediaResponse ValidateImage(string extension, long fileSize);
        

        public MediaResponse ValidateImageSize(string extension);
       

        public MediaResponse UploadCompressedImage(Stream sourcePath);
       

        public Task<MediaResponse> SaveProductImage(CreateProductDTO objectFile);

        public Task<MediaResponse> SaveImage(IFormFile imageObject, string reference,string entity,string images);
        
    }
}
