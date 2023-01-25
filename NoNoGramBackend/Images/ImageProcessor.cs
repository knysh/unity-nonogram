using SkiaSharp;
using System.Reflection;

namespace NoNoGramBackend
{
    public class ImageProcessor
    {
        public void Process(string imageName)
        {
            var image = GetBitmap(imageName);
            ImageConvertor.ConverToBWImage(image, imageName);
        }

        public SquareInfos GetSquareInfos(int squareSize, string imageName)
        {
            var image = GetBitmap(imageName);
            return ImageConvertor.GetSquareInfos(squareSize, image);
        }

        private SKBitmap GetBitmap(string imageName)
        {
            var resourceID = $"NoNoGramBackend.Resources.{imageName}.png";
            var assembly = GetType().GetTypeInfo().Assembly;
            using var stream = assembly.GetManifestResourceStream(resourceID);
            return SKBitmap.Decode(stream);
        }
    }
}
