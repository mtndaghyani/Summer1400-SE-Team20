using System.Collections.Generic;
using System.Linq;
using SearchEngine.Database;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class DatabaseInvertedIndex : IInvertedIndex<string, Document>
    {
        public bool ContainsKey(string key)
        {
            using var indexingContext = new IndexingContext();
            var contains = indexingContext.WordDocumentsPairs.Any(x => x.Word == key);
            indexingContext.SaveChanges();
            return contains;
        }

        public HashSet<Document> Get(string key)
        {
            using var indexingContext = new IndexingContext();
            WordDocumentsPair pair = indexingContext.WordDocumentsPairs.Single(x => x.Word == key);
            return new HashSet<Document>(pair.Documents);
        }

        public void Add(string key, Document value)
        {
            using var indexingContext = new IndexingContext();
            WordDocumentsPair pair = indexingContext.WordDocumentsPairs.Single(x => x.Word == key);
            if (pair == null)
            {
                pair = new WordDocumentsPair() {Word = key, Documents = new List<Document>() {value}};
                indexingContext.WordDocumentsPairs.Add(pair);
            }
            else
            {
                pair.Documents.Add(value);
                indexingContext.WordDocumentsPairs.Update(pair);
            }

            indexingContext.SaveChanges();
        }
    }
}