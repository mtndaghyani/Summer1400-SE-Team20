using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class DictionaryInvertedIndex : IInvertedIndex<string, int>
    {
        private readonly Dictionary<string, HashSet<int>> _invertedIndex = new();

        public bool ContainsKey(string key)
        {
            return _invertedIndex.ContainsKey(key);
        }

        public HashSet<int> Get(string key)
        {
            return _invertedIndex[key];
        }

        public void Add(string key, int value)
        {
            if (_invertedIndex.ContainsKey(key))
            {
                _invertedIndex[key].Add(value);
            }
            else
            {
                _invertedIndex[key] = new HashSet<int>() {value};
            }
        }
    }
}