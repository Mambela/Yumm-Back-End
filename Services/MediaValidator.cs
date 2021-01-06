using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Services.Response;

namespace Yumm_Back_End.Services
{
    public class MediaValidator
    {

        private MediaResponse _mediaResponse;
        private const int ImageMaxSize = 2620;
 

        public MediaValidator()
        {
            _mediaResponse = new MediaResponse();
        }

        public MediaResponse ValidateImage(string extension, long fileSize)
        {

            _mediaResponse.Success = true;

            /*
             * Retrieve from the end of the extension to the an / so that we get just the type of the image
             * **/

            extension = extension.Substring(extension.IndexOf("/") + 1, extension.Length - (extension.IndexOf("/") + 1));

            //_mediaResponse.Message = extension;


            var supportedTypes = new[] { "png", "jpg", "jpeg" };

            if (!supportedTypes.Contains<string>(extension))
            {
                _mediaResponse.Success = false;
                _mediaResponse.Message = "Error! Only upload of images allowed";

                return _mediaResponse;
            }


            //Maximum size allowed is 2.6 Mb

            if ((fileSize/1048) > ImageMaxSize)
               {
                _mediaResponse.Success = false;
                _mediaResponse.Message = "Error!  The file size is too large";

                return _mediaResponse;
               }


                _mediaResponse.Message = "Image has passed all the validations ";
            //Check if the File size is within size limit. 


            // Check if the file extension is within documents required. 
            //Check if the file is a jpeg, jpg or Png. 


            return _mediaResponse;
        }

        public MediaResponse ValidateImageSize(string extension)
        {

            //_mediaResponse = new MediaResponse();

            _mediaResponse.Success = true;
            _mediaResponse.Message = "Testin the response "+extension;

            return _mediaResponse;
        }

        
    }
}
