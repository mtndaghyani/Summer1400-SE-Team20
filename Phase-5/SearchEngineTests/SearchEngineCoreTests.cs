using System;
using System.Collections.Generic;
using NSubstitute;
using SearchEngine.Classes;
using SearchEngine.Interfaces;
using Xunit;

namespace SearchEngineTests
{
    public class SearchEngineCoreTests
    {
        private readonly IIndexer _indexer = Substitute.For<IIndexer>();
        private IInvertedIndex<string, int> _invertedIndexMock = Substitute.For<IInvertedIndex<string, int>>();
        
        public SearchEngineCoreTests()
        {
            _invertedIndexMock.Get("salam").Returns(new HashSet<int>(new int[] {1, 3}));
            _invertedIndexMock.Get("jinks").Returns(new HashSet<int>(new int[] {1, 26, 30}));
            _invertedIndexMock.ContainsKey("salam").Returns(true);
            _invertedIndexMock.ContainsKey("jinks").Returns(true);
            _indexer.Stem(Arg.Any<string>()).Returns(i => i[0]);
            _indexer.GetInvertedIndex().Returns(_invertedIndexMock);
        }
        
        [Fact]
        public void TestSearch_WHEN_salam_EXPECTED_oneAndThree(){
            string toSearch = "salam";
            List<int> expected = new List<int>(new int[] {1, 3});
            SearchEngineCore searchEngine = new SearchEngineCore(_indexer);
            HashSet<int> searchResult = searchEngine.Search(toSearch);
            Assert.Equal(expected, searchResult);
        }
        
        [Fact]
        public void TestSearch_WHEN_salamPlusJinks_EXPECTED_one(){
            String toSearch = "salam +jinks";
            List<int> expected = new List<int>(new int[]{1});
            SearchEngineCore searchEngine = new SearchEngineCore(_indexer);
            HashSet<int> searchResult = searchEngine.Search(toSearch);
            Assert.Equal(expected, searchResult);
        }

        [Fact]
        public void TestSearch_WHEN_jinksMinusSalam_EXPECTED_one(){
            String toSearch = "-salam jinks";
            List<int> expected = new List<int>(new int[]{26, 30});
            SearchEngineCore searchEngine = new SearchEngineCore(_indexer);
            HashSet<int> searchResult = searchEngine.Search(toSearch);
            Assert.Equal(expected, searchResult);
        }
    }
}