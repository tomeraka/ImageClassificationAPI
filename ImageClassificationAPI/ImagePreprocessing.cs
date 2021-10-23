using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageClassificationAPI
{
    public class ImagePreprocessing
    {
        public static Image<Rgb24> Preprocess(MemoryStream inputImageStream)
        {
            using var image = Image.Load<Rgb24>(inputImageStream.ToArray(), out IImageFormat imageFormat);

            return image;
        }
    }
}
