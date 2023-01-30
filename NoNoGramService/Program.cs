using Microsoft.AspNetCore.Mvc;
using NoNoGramBackend;

internal class Program
{
    private static readonly List<string> Images = new() { "lizard", "dragonfly", "hedgehog", "sheep" };

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Info!");
        app.MapGet("/random_game", ([FromQuery(Name = "squareSize")] int squareSize) =>
        {
            var random = new Random();
            var imageProcessor = new ImageProcessor();
            return imageProcessor.GetSquareInfos(squareSize, Images[random.Next(Images.Count)]);
        });

        app.Run();
    }
}