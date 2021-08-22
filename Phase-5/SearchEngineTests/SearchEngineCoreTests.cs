using System;
using System.Collections.Generic;
using NSubstitute;
using SearchEngine.Classes.Core;
using SearchEngine.Classes.IO.Database.Models;
using SearchEngine.Interfaces.Indexers;
using Xunit;

namespace SearchEngineTests
{
    public class SearchEngineCoreTests
    {
        private readonly IWordProcessor _wordProcessorMock = Substitute.For<IWordProcessor>();
        private IInvertedIndex<string, Document> _invertedIndexMock = Substitute.For<IInvertedIndex<string, Document>>();

        public SearchEngineCoreTests()
        {
            _invertedIndexMock.Get("salam").Returns(new HashSet<Document>(new Document[]
            {
                new(){DocumentNumber = 1},
                new(){DocumentNumber = 3}
            }));
            _invertedIndexMock.Get("jinks").Returns(new HashSet<Document>(new Document[]
            {
                new(){DocumentNumber = 1},
                new(){DocumentNumber = 26},
                new(){DocumentNumber = 30}
            }));
            _invertedIndexMock.Get("whatsUp").Returns(new HashSet<Document>());
            _invertedIndexMock.ContainsKey("salam").Returns(true);
            _invertedIndexMock.ContainsKey("jinks").Returns(true);
            _wordProcessorMock.ProcessWord(Arg.Any<string>()).Returns(i => i[0]);
        }
        
        [Fact]
        public void TestSearch_WHEN_whatsUp_EXPECTED_empty(){
            string toSearch = "whatsUp";
            List<int> expected = new List<int>(new int[] {});
            SearchEngineCore searchEngine = new SearchEngineCore(_wordProcessorMock, _invertedIndexMock);
            HashSet<Document> searchResult = searchEngine.Search(toSearch);
            AssertEqualDocumentEnumerable(expected, searchResult);
        }
        
        [Fact]
        public void TestSearch_WHEN_salam_EXPECTED_oneAndThree(){
            string toSearch = "salam";
            List<int> expected = new List<int>(new int[] {1, 3});
            SearchEngineCore searchEngine = new SearchEngineCore(_wordProcessorMock, _invertedIndexMock);
            HashSet<Document> searchResult = searchEngine.Search(toSearch);
            AssertEqualDocumentEnumerable(expected, searchResult);
        }
        
        [Fact]
        public void TestSearch_WHEN_salamPlusJinks_EXPECTED_one(){
            String toSearch = "salam +jinks";
            List<int> expected = new List<int>(new int[]{1});
            SearchEngineCore searchEngine = new SearchEngineCore(_wordProcessorMock, _invertedIndexMock);
            HashSet<Document> searchResult = searchEngine.Search(toSearch);
            AssertEqualDocumentEnumerable(expected, searchResult);
        }

        [Fact]
        public void TestSearch_WHEN_jinksMinusSalam_EXPECTED_one(){
            String toSearch = "-salam jinks";
            List<int> expected = new List<int>(new int[]{26, 30});
            SearchEngineCore searchEngine = new SearchEngineCore(_wordProcessorMock, _invertedIndexMock);
            HashSet<Document> searchResult = searchEngine.Search(toSearch);
            AssertEqualDocumentEnumerable(expected, searchResult);
        }
        
        private void AssertEqualDocumentEnumerable(ICollection<int> expected, HashSet<Document> result)
        {
            Assert.Equal(expected.Count, result.Count);
            foreach (Document doc in result)
            {
                if (!expected.Contains(doc.DocumentNumber))
                {
                    Assert.True(false, "Hashset does not contain expected value.");
                }
            }
        }
    }
}