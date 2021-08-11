using System.Collections.Generic;
using SearchEngine.Database;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class DictionaryInvertedIndex : IInvertedIndex<string, Document>
    {
        private readonly Dictionary<string, HashSet<Document>> _invertedIndex = new();

        public bool ContainsKey(string key)
        {
            return _invertedIndex.ContainsKey(key);
        }

        public HashSet<Document> Get(string key)
        {
            return _invertedIndex[key];
        }

        public void Add(string key, Document value)
        {
            if (_invertedIndex.ContainsKey(key))
            {
                _invertedIndex[key].Add(value);
            }
            else
            {
                _invertedIndex[key] = new HashSet<Document>() {value};
            }
        }
    }
}