using System.Collections.Generic;
using System.IO;
using SearchEngine.Classes;
using SearchEngine.Interfaces;
using Xunit;

namespace SearchEngineTests
{
    public class FileReaderTests
    {
        private readonly string _dataSetDir;

        public FileReaderTests()
        {
            _dataSetDir = Directory.GetCurrentDirectory() + "../../../../testDocs";
        }

        [Fact]
        public void TestReadFiles()
        {
            IReader reader = new FileReader(_dataSetDir);
            Assert.Equal(GetContents(), reader.Read());
        }

        private List<string> GetContents()
        {
            return new List<string>()
            {
                "Video provides a powerful way to help you prove your point.", 
                "To make your document look professionally produced, Word provides header.",
                "Themes and styles also help keep your document coordinated."
            };
        }
    }
}