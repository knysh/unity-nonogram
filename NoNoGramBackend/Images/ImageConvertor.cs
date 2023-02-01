using NoNoGramBackend.Models;
using NoNoGramBackend.Squares;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NoNoGramBackend
{
    public class ImageConvertor
    {
        public static void ConverToBWImage(SKBitmap sourceImage, string imageName)
        {
            using var canvas = new SKCanvas(sourceImage);
            var newPixels = new List<SKColor>();
            sourceImage.Pixels.ToList().ForEach(pixel =>
            {
                var brightness = 0.2126 * pixel.Red + 0.7152 * pixel.Green + 0.0722 * pixel.Blue;
                if (brightness > 240)
                {
                    newPixels.Add(pixel.WithBlue(255).WithGreen(255).WithRed(255));
                }
                else
                {
                    newPixels.Add(pixel.WithBlue(0).WithGreen(0).WithRed(0));
                }
            });

            sourceImage.Pixels = newPixels.ToArray();
            using var data = sourceImage.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(Path.Combine($"{imageName}BW.png"));
            data.SaveTo(stream);
        }

        public static SquareInfos GetSquareInfos(int squareSize, SKBitmap sourceImage)
        {
            var squareInfos = new List<SquareInfoRow>();
            var xSquares = GetNumberOfSquares(squareSize, sourceImage.Width);
            var xOffset = GetOffset(squareSize, sourceImage.Width);
            var ySquares = GetNumberOfSquares(squareSize, sourceImage.Height);
            var yOffset = GetOffset(squareSize, sourceImage.Height);

            for (int i = 0; i < xSquares; i++)
            {
                var squareInfosRow = new List<SquareInfo>();
                for (int j = 0; j < ySquares; j++)
                {
                    var rectI = SKRectI.Create(xOffset + squareSize * i, yOffset + squareSize * j, squareSize, squareSize);
                    using var squareImage = new SKBitmap(new SKImageInfo(squareSize, squareSize));
                    var ok = sourceImage.ExtractSubset(squareImage, rectI);
                    squareInfosRow.Add(new SquareInfo
                    {
                        Color = GetSquareColor(squareImage),
                        X = i,
                        Y = 0-j
                    });
                }

                squareInfos.Add(new SquareInfoRow()
                {
                    Row = squareInfosRow
                });
            }

            return new SquareInfos()
            {
                Squares = squareInfos,
                ColumnCouners = SquareUtil.GetColumnCouners(squareInfos),
                RowCouners = null
            };
        }

        private static Color GetSquareColor (SKBitmap image)
        {
            var totalBlue = 0;
            var totalRed = 0;
            var totalGreen = 0;

            image.Pixels.ToList().ForEach(pixel =>
            {
                totalBlue += pixel.Blue;
                totalRed += pixel.Red;
                totalGreen += pixel.Green;
            });

            var brightness = (0.2126 * totalRed + 0.7152 * totalGreen + 0.0722 * totalBlue)/image.Pixels.Length;
            if (brightness > 240)
            {
                return Color.WHITE;
            }
            else
            {
                return Color.BLACK;
            }
        }

        private static int GetNumberOfSquares (int squareSize, int imageSize)
        {
            return imageSize / squareSize;
        }

        private static int GetOffset(int squareSize, int imageSize)
        {
            int numberOfSquares = imageSize / squareSize;
            if (numberOfSquares < 10)
            {
                throw new InvalidDataException($"Square size {squareSize} is too big for image size {imageSize}.");
            }

            var offset = (imageSize - numberOfSquares * squareSize) / 2;
            return offset;
        }
    }
}
