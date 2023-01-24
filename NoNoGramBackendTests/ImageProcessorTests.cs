using NoNoGramBackend;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class ImageProcessorTests
    {
        [Test]
        public void BlackAndWhite()
        {
            var imageProcessor = new ImageProcessor();
            imageProcessor.Process("dragonfly");
            imageProcessor.Process("hedgehog");
            imageProcessor.Process("lizard");
            imageProcessor.Process("sheep");
        }

        [Test]
        public void GetSquareInfosTest()
        {
            var imageProcessor = new ImageProcessor();
            var squareInfos = imageProcessor.GetSquareInfos(8, "dragonfly");
            Assert.AreEqual(20, squareInfos.Count);
            Assert.AreEqual(21, squareInfos.First().Count);
            Assert.IsTrue(squareInfos.SelectMany(row => row).Any(info => info.Color.Equals(Color.WHITE)));
            Assert.IsTrue(squareInfos.SelectMany(row => row).Any(info => info.Color.Equals(Color.BLACK)));
        }
    }
}