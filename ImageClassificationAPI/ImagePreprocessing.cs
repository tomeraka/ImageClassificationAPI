using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageClassificationAPI
{
    public class ImagePreprocessing
    {
        public static DenseTensor<float> Preprocess(MemoryStream inputImageStream)
        {
            using var image = Image.Load<Rgb24>(inputImageStream.ToArray(), out IImageFormat imageFormat);

            using var preprocessedImageStream = new MemoryStream();

            image.Mutate(x =>
            {
                x.Resize(new ResizeOptions()
                {
                    Size = new Size(256, 256),
                    Mode = ResizeMode.Crop
                });
            });
            image.Save(preprocessedImageStream, imageFormat);

            var tensor = new DenseTensor<float>(new[] { 1, image.Height, image.Width, 3 });

            for (int i = 0; i < image.Height; i++)
            {
                var pixelRowSpan = image.GetPixelRowSpan(i);
                for (int j = 0; j < image.Width; j++)
                {
                    var pixel = pixelRowSpan[j];
                    tensor[0, i, j, 0] = (pixel.R - 127) / 128f;
                    tensor[0, i, j, 1] = (pixel.G - 127) / 128f;
                    tensor[0, i, j, 2] = (pixel.B - 127) / 128f;
                }
            }
            return tensor;
        }
    }
}
