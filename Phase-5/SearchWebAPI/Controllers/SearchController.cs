using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public List<int> Query(string toSearch)
        {
            return new(_searchEngineCore.Search(toSearch).Select(x => x.DocumentNumber));
        }
    }
}