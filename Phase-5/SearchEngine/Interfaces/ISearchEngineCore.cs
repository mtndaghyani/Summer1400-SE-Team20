using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface ISearchEngineCore
    {
        public HashSet<int> search(string statement);
    }
}