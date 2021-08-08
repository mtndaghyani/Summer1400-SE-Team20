using System.Collections.Generic;
using NSubstitute;
using SearchEngine;
using SearchEngine.Classes;
using SearchEngine.Interfaces;
using Xunit;

namespace SearchEngineTests
{
    public class InvertedIndexTests
    {
        private IReader _readerMock;
        private IWordProcessor _wordProcessorMock;
        private IInvertedIndex _invertedIndex;

        public InvertedIndexTests()
        {
            _readerMock = Substitute.For<IReader>();
            _wordProcessorMock = Substitute.For<IWordProcessor>();
            
            _readerMock.Read().Returns(GetContents());
            _wordProcessorMock.ProcessWord("Video.").Returns("video");
            _wordProcessorMock.ProcessWord("Online,").Returns("online");
            _wordProcessorMock.ProcessWord("Theme;").Returns("theme");
            _wordProcessorMock.ProcessWord("video").Returns("video");

            _invertedIndex = new InvertedIndex(_readerMock, _wordProcessorMock);
        }

        [Fact]
        public void TestGetTokens()
        {
            Assert.Equal(GetCorrectTokens(), _invertedIndex.GetTokens());
        }

        [Fact]
        public void TestSetUpDictionary()
        {
            var expected = GetCorrectDictionary();
            var actual = _invertedIndex.GetDictionary();
            Assert.Equal(3, actual.Count);
            Assert.Equal(expected.Keys, actual.Keys);
            foreach (var (key, value) in expected)
            {
                Assert.Equal(actual[key], value);
            }

        }

        private Dictionary<string,HashSet<int>> GetCorrectDictionary()
        {
            var correctDictionary = new Dictionary<string, HashSet<int>>();
            var s1 = new HashSet<int>(){1, 3};
            var s2 = new HashSet<int>(){2};
            var s3 = new HashSet<int>(){3};
            correctDictionary.Add("video", s1);
            correctDictionary.Add("online", s2);
            correctDictionary.Add("theme", s3);
            return correctDictionary;
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