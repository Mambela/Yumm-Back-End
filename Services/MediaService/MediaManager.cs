using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Services.Logger;
using Yumm_Back_End.Services.Response;


namespace Yumm_Back_End.Services.MediaService
{
    public class MediaManager: IMediaManager
    {

        private MediaResponse _mediaResponse;
        private const int ImageMaxSize = 2620;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILoggerService _loggerService;


        public MediaManager(IWebHostEnvironment webHostenvironment, ILoggerService loggerService)
        {
            _webHostEnvironment = webHostenvironment;
            _mediaResponse = new MediaResponse();
            _loggerService = loggerService;
        }

        public MediaResponse ValidateImage(string extension, long fileSize)
        {

            _mediaResponse.isSucess = true;

            /*
             * Retrieve from the end of the extension to the an / so that we get just the type of the image
             * **/

            extension = extension.Substring(extension.IndexOf("/") + 1, extension.Length - (extension.IndexOf("/") + 1));

            //_mediaResponse.Message = extension;


            var supportedTypes = new[] { "png", "jpg", "jpeg" };

            if (!supportedTypes.Contains(extension))
            {
                _mediaResponse.isSucess = false;
                _mediaResponse.imagePath = "Error! Only upload of images allowed";

                return _mediaResponse;
            }
             

            //Maximum size allowed is 2.6 Mb

            if (fileSize / 4192 > ImageMaxSize)
            {
                _mediaResponse.isSucess = false;
                _mediaResponse.imagePath = "Error!  The file size is too large";

                return _mediaResponse;
            }


            _mediaResponse.imagePath = "Image has passed all the validations ";
            //Check if the File size is within size limit. 


            // Check if the file extension is within documents required. 
            //Check if the file is a jpeg, jpg or Png. 


            return _mediaResponse;
        }

        public MediaResponse ValidateImageSize(string extension)
        {

            //_mediaResponse = new MediaResponse();

            _mediaResponse.isSucess = true;
            _mediaResponse.imagePath = "Testin the response " + extension;

            return _mediaResponse;
        }

        public MediaResponse UploadCompressedImage(Stream sourcePath)
        {
            try
            {

                using (MagickImage image = new MagickImage(sourcePath))
                {
                    image.Format = image.Format;
                   
                    /*
                     * To resize the image there are specific measures that need to be taken. 
                     * 
                     * 1st the Size of the image needs to be reduce by precentage. 
                     *
                     * Find the size of the image compare to the default size. 
                     * See how much bigger than the default size it is. 
                     * Then reduce it by percentage. 
                     * The percentage of the size should amount to the length and the height the new images will be. 
                     * If Calculation is that the size should be reduced then reeduce dimension by the percentage that image has to be reduced by. 
                     * 
                     * **/
                }


                    return null;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<MediaResponse> SaveProductImage(CreateProductDTO objectFile)
        {
            //Few Classes required to Save the image correctly;


            //Class to generate Image uniqueId
            Generators gen = new Generators();


            //Class responsible for responding the success of the upload
            MediaResponse mdResponse = new MediaResponse();

            //Class Responsible to validate an Image



            //Validate the image
            mdResponse = ValidateImage(objectFile.Files.ContentType, objectFile.Files.Length);

            if (!mdResponse.isSucess)
                return mdResponse;

            //Reduce size.






            Console.WriteLine(objectFile.Files.Length / 1048);

            Console.WriteLine(mdResponse.imagePath);


            string reference = objectFile.RestaurantId.ToString();
            string resourceRoot = "\\resources\\", _entity = "\\" + "restaurant" + "\\", images = "\\images\\";


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


                    string samplePath = uniqueID.Trim() + objectFile.Files.FileName;
                    samplePath = samplePath.Trim();



                    using (FileStream fileStream = File.Create(_root + samplePath))
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

                _loggerService.LogError("Error image " + ex.Message);

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<MediaResponse> SaveImage(IFormFile imageObject, string _reference, string entity, string images)
        {
            //Few Classes required to Save the image correctly;


            //Class to generate Image uniqueId
            Generators gen = new Generators();


            //Class responsible for responding the success of the upload
            MediaResponse mdResponse = new MediaResponse();

            //Class Responsible to validate an Image

            //Validate the image
            mdResponse = ValidateImage(imageObject.ContentType, imageObject.Length);

            if (!mdResponse.isSucess)
                return null;

            //Reduce size.

            string reference = _reference.ToString();
            string resourceRoot = "\\resources\\";
            //, _entity = "\\" + "restaurant" + "\\", images = "\\images\\";


            try
            {
                if (imageObject.Length > 0)
                {
                    string _root = _webHostEnvironment.WebRootPath + resourceRoot + entity + reference + images;


                    if (!Directory.Exists(_root))
                    {
                        Directory.CreateDirectory(_root);
                    }


                    Console.WriteLine(_root);
                    string uniqueID = gen.GenerateUniqueNumber();


                    string samplePath = uniqueID.Trim() + imageObject.FileName;
                    samplePath = samplePath.Trim();



                    using (FileStream fileStream = File.Create(_root + samplePath))
                    {
                        await imageObject.CopyToAsync(fileStream);
                        fileStream.Flush();

                        mdResponse.imagePath = samplePath;

                        return mdResponse;
                    }

                }

                return null;
            }
            catch (Exception ex)
            {

                _loggerService.LogError("Error image " + ex.Message);

                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
