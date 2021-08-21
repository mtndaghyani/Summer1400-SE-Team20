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

        public IndexerTests()
        {
            _readerMock = Substitute.For<IReader>();
            _wordProcessorMock = Substitute.For<IWordProcessor>();
            
            _readerMock.Read().Returns(GetContents());
            _wordProcessorMock.ProcessWord("Video.").Returns("video");
            _wordProcessorMock.ProcessWord("Online,").Returns("online");
            _wordProcessorMock.ProcessWord("Theme;").Returns("theme");
            _wordProcessorMock.ProcessWord("video").Returns("video");

            _indexer = new Indexer(_readerMock, _wordProcessorMock);
        }

        [Fact]
        public void TestGetTokens()
        {
            Assert.Equal(GetCorrectTokens(), _indexer.GetDocumentsTokens());
        }

        [Fact]
        public void TestSetUpInvertedIndex()
        {
            var expected = GetCorrectInvertedIndex();
            var actual = _indexer.GetInvertedIndex();
            Assert.Equal(3, actual.Count);
            Assert.Equal(expected.Keys, actual.Keys);
            foreach (var (key, value) in expected)
            {
                Assert.Equal(value, actual[key]);
            }

        }

        private Dictionary<string,HashSet<int>> GetCorrectInvertedIndex()
        {
            var correctInvertedIndex = new Dictionary<string, HashSet<int>>();
            var s1 = new HashSet<int>(){1, 3};
            var s2 = new HashSet<int>(){2};
            var s3 = new HashSet<int>(){3};
            correctInvertedIndex.Add("video", s1);
            correctInvertedIndex.Add("online", s2);
            correctInvertedIndex.Add("theme", s3);
            return correctInvertedIndex;
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