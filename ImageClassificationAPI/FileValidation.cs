using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassificationAPI
{
    public static class FileValidation
    {
        public static string VerifyInputFile (IFormFile imageFile)
        {            
            if (imageFile == null)
                return "A file wasn't uploaded. Please upload an image.";

            using var validationStream = new MemoryStream();
            imageFile.CopyTo(validationStream);
            byte[] isImage = validationStream.ToArray();

            if (!isImage.IsValidImage())
                return "Wrong file format. Supported formats are jpg, png, gif, bmp and tga.";

            if (imageFile.Length > 10485760)
                return "Image size is too large. Please upload an image with a maximum size of 10MB.";

            return "ok";
        }

        public static bool IsValidImage(this byte[] image)
        {
            var imageFormat = GetImageFormat(image);
            return !(imageFormat == ImageFormat.unknown);
        }

        public enum ImageFormat
        {
            bmp,
            gif,
            tga,
            png,
            jpeg,
            unknown
        }

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            // supported image formats for SixLabors
            var bmp = Encoding.ASCII.GetBytes("BM");
            var gif = Encoding.ASCII.GetBytes("GIF");
            var tga = Encoding.ASCII.GetBytes("TGA");
            var png = new byte[] { 137, 80, 78, 71 };
            var jpeg = new byte[] { 255, 216, 255, 224 };
            var jpeg2 = new byte[] { 255, 216, 255, 225 };

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (tga.SequenceEqual(bytes.Take(tga.Length)))
                return ImageFormat.tga;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }
    }
}
