using System.Collections.Generic;
using SearchEngine.Classes.IO.Database.Models;
using SearchEngine.Interfaces.Indexers;

namespace SearchEngine.Classes.Indexers
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
            return _invertedIndex.ContainsKey(key) ? _invertedIndex[key] : new HashSet<Document>();
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