using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NoNoGramBackend
{
    public class ImageConvertor
    {
        public static void ConverToBWImage(SKBitmap SourceImage, string imageName)
        {
            using var canvas = new SKCanvas(SourceImage);
            var newPixels = new List<SKColor>();
            SourceImage.Pixels.ToList().ForEach(pixel =>
            {
                var brightness = 0.2126 * pixel.Red + 0.7152 * pixel.Green + 0.0722 * pixel.Blue;
                if (brightness > 240)
                {
                    newPixels.Add(pixel.WithBlue(255).WithGreen(255).WithRed(255));  
                } else
                {
                    newPixels.Add(pixel.WithBlue(0).WithGreen(0).WithRed(0));
                }
            });

            SourceImage.Pixels = newPixels.ToArray();
            using var data = SourceImage.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(Path.Combine($"{imageName}BW.png"));
            data.SaveTo(stream);
        }
    }
}
