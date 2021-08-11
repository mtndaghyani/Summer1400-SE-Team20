using System.Collections.Generic;
using SearchEngine.Database;

namespace SearchEngine.Interfaces
{
    public interface ISearchEngineCore
    {
        HashSet<Document> Search(string statement);
    }
}