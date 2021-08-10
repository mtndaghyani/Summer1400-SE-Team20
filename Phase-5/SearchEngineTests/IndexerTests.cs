using System.Collections.Generic;
using NSubstitute;
using SearchEngine.Classes;
using SearchEngine.Interfaces;
using Xunit;

namespace SearchEngineTests
{
    public class IndexerTests
    {
        private IReader _readerMock;
        private IWordProcessor _wordProcessorMock;
        private IIndexer _indexer;
        private IInvertedIndex<string, int> _invertedIndexMock;
        public IndexerTests()
        {
            _readerMock = Substitute.For<IReader>();
            _wordProcessorMock = Substitute.For<IWordProcessor>();
            _invertedIndexMock = Substitute.For<IInvertedIndex<string, int>>();
            
            _readerMock.Read().Returns(GetContents());
            _wordProcessorMock.ProcessWord("Video.").Returns("video");
            _wordProcessorMock.ProcessWord("Online,").Returns("online");
            _wordProcessorMock.ProcessWord("Theme;").Returns("theme");
            _wordProcessorMock.ProcessWord("video").Returns("video");
            
            _indexer = new Indexer(_readerMock, _wordProcessorMock, _invertedIndexMock);
        }

        [Fact]
        public void TestGetTokens()
        {
            Assert.Equal(GetCorrectTokens(), _indexer.GetDocumentsTokens());
        }

        private List<string> GetContents()
        {
            var result = new List<string>();
            result.Add("Video.");
            result.Add("Online,   ");
            result.Add("Theme; video");
            return result;
        }

        private List<List<string>> GetCorrectTokens()
        {
            var result = new List<List<string>>();
            var tokens1 = new List<string>() {"video"};
            var tokens2 = new List<string>() {"online"};
            var tokens3 = new List<string>() {"theme", "video"};
            result.Add(tokens1);
            result.Add(tokens2);
            result.Add(tokens3);
            return result;
        }
        
        
    }
}