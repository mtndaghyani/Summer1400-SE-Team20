using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface ISearchEngineCore
    {
        HashSet<int> Search(string statement);
    }
}