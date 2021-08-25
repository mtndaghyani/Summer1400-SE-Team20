using System.Collections.Generic;
using SearchEngine.Classes.IO.Database.Models;

namespace SearchEngine.Interfaces.Core
{
    public interface ISearchEngineCore
    {
        HashSet<Document> Search(string statement);
    }
}