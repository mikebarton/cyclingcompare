using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats;

namespace bikecompare.imaging.handlers.Services
{
    public class ImageService 
    {
        public void ResizeImage(Stream sourceStream, Stream destinationStream, int canvasHeight, int canvasWidth)
        {
            Image<Rgba32> image = (Image<Rgba32>)Image.Load(sourceStream);
            var originalHeight = image.Height;
            var originalWidth = image.Width;


            double ratioX = (double)canvasWidth / (double)originalWidth;
            double ratioY = (double)canvasHeight / (double)originalHeight;
            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // now we can get the new height and width
            int targetHeight = Convert.ToInt32(originalHeight * ratio);
            int targetWidth = Convert.ToInt32(originalWidth * ratio);

            image.Mutate(context =>
            {
                context.Resize(targetWidth, targetHeight);
            });

            image.Save(destinationStream, new JpegEncoder());
        }
    }
}
