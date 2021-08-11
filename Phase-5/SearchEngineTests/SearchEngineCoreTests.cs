using System;
using System.Collections.Generic;
using NSubstitute;
using SearchEngine.Classes;
using SearchEngine.Database;
using SearchEngine.Interfaces;
using Xunit;

namespace SearchEngineTests
{
    public class SearchEngineCoreTests
    {
        private readonly IIndexer<string, Document> _indexer = Substitute.For<IIndexer<string, Document>>();
        private IInvertedIndex<string, Document> _invertedIndexMock = Substitute.For<IInvertedIndex<string, Document>>();

        public SearchEngineCoreTests()
        {
            _invertedIndexMock.Get("salam").Returns(new HashSet<Document>(new Document[]
            {
                new(){DocumentId = 1},
                new(){DocumentId = 3}
            }));
            _invertedIndexMock.Get("jinks").Returns(new HashSet<Document>(new Document[]
            {
                new(){DocumentId = 1},
                new(){DocumentId = 26},
                new(){DocumentId = 30}
            }));
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
            HashSet<Document> searchResult = searchEngine.Search(toSearch);
            AssertEqualDocumentEnumerable(expected, searchResult);
        }
        
        [Fact]
        public void TestSearch_WHEN_salamPlusJinks_EXPECTED_one(){
            String toSearch = "salam +jinks";
            List<int> expected = new List<int>(new int[]{1});
            SearchEngineCore searchEngine = new SearchEngineCore(_indexer);
            HashSet<Document> searchResult = searchEngine.Search(toSearch);
            AssertEqualDocumentEnumerable(expected, searchResult);
        }

        [Fact]
        public void TestSearch_WHEN_jinksMinusSalam_EXPECTED_one(){
            String toSearch = "-salam jinks";
            List<int> expected = new List<int>(new int[]{26, 30});
            SearchEngineCore searchEngine = new SearchEngineCore(_indexer);
            HashSet<Document> searchResult = searchEngine.Search(toSearch);
            AssertEqualDocumentEnumerable(expected, searchResult);
        }
        
        private void AssertEqualDocumentEnumerable(ICollection<int> expected, HashSet<Document> result)
        {
            Assert.Equal(expected.Count, result.Count);
            foreach (Document doc in result)
            {
                if (!expected.Contains(doc.DocumentId))
                {
                    Assert.True(false, "Hashset does not contain expected value.");
                }
            }
        }
    }
}