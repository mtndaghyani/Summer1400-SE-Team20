using SearchEngine.Classes;
using Xunit;

namespace SearchEngineTests
{
    public class WordProcessorTests
    {
        [Fact]
        public void TestProcessWord()
        {
            var processor = new WordProcessor();
            Assert.Equal("food", (processor.ProcessWord(".,Food;")));
        }
    }
}