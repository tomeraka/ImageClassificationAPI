using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageClassificationAPI
{
    public class FileValidation
    {
        public static bool IsImageFormat(string FileName)
        {
            string[] validFormats = { "jpg", "jpeg", "png", "gif", "bmp" };
            bool result = validFormats.Any(x => FileName.EndsWith(x));
            return result;
        }

        public static string VerifyInputFile (IFormFile imageFile)
        {            
            try
            {
                if (imageFile == null)
                    return "A file wasn't uploaded. Please upload an image.";

                if (!IsImageFormat(imageFile.FileName))
                    return "Wrong image file format. Supported formats are .jpg, .png, .gif and .bmp";

                if (imageFile.Length > 10485760)
                    return "Image size is too large. Please upload an image with a maximum size of 10MB.";

                else
                    return "ok";
            }
            catch (Exception)
            {
                return "Couldn't find an image.";
            }
        }
    }
}
