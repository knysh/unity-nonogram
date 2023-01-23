using NoNoGramBackend;
using NUnit.Framework;

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
    }
}