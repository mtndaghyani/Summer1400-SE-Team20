using System.Collections.Generic;
using System.Linq;
using SearchEngine.Database;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class DatabaseInvertedIndex : IInvertedIndex<string, int>
    {
        public bool ContainsKey(string key)
        {
            using var indexingContext = new IndexingContext();
            var contains = indexingContext.WordDocumentsPairs.Any(x => x.Word == key);
            indexingContext.SaveChanges();
            return contains;
        }

        public HashSet<int> Get(string key)
        {
            using var indexingContext = new IndexingContext();
            WordDocumentsPair pair = indexingContext.WordDocumentsPairs.Single(x => x.Word == key);
            return new HashSet<int>(pair.DocumentIDs);
        }

        public void Add(string key, int value)
        {
            using var indexingContext = new IndexingContext();
            WordDocumentsPair pair = indexingContext.WordDocumentsPairs.Single(x => x.Word == key);
            if (pair == null)
            {
                pair = new WordDocumentsPair() {Word = key, DocumentIDs = new List<int>() {value}};
                indexingContext.WordDocumentsPairs.Add(pair);
            }
            else
            {
                pair.DocumentIDs.Add(value);
                indexingContext.WordDocumentsPairs.Update(pair);
            }

            indexingContext.SaveChanges();
        }
    }
}