using System.Collections.Generic;
using NSubstitute;
using SearchEngine.Interfaces.Core;
using SearchWebAPI.Controllers;
using Xunit;
using Document = SearchEngine.Classes.IO.Database.Models.Document;

namespace SearchEngineTests.WebAPITests
{
    public class SearchControllerTests
    {
        private readonly SearchController _searchController;

        public SearchControllerTests()
        {
            var searchEngineMock = Substitute.For<ISearchEngineCore>();
            var doc = new Document();
            searchEngineMock.Search(Arg.Any<string>()).Returns(new HashSet<Document>() {doc});
            _searchController = new SearchController(searchEngineMock);
        }

        [Fact]
        public void TestQuery()
        {
            var result = _searchController.Query("test");
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}