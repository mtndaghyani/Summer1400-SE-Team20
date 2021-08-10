using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class DatabaseInvertedIndex:IInvertedIndex<string, int>
    {
        public bool ContainsKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public HashSet<int> Get(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}