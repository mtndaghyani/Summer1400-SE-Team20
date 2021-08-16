using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SearchEngine.Classes.IO.Database.Models;
using SearchEngine.Interfaces.Core;

namespace SearchWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SearchController : Controller
    {
        private readonly ISearchEngineCore _searchEngineCore;

        public SearchController(ISearchEngineCore searchEngineCore)
        {
            _searchEngineCore = searchEngineCore;
        }

        [HttpGet()]
        public List<Document> Query(string toSearch)
        {
            return new(_searchEngineCore.Search(toSearch));
        }
    }
}