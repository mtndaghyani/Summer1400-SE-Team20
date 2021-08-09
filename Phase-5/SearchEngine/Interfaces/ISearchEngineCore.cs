using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface ISearchEngineCore
    {
        public HashSet<int> Search(string statement);
    }
}