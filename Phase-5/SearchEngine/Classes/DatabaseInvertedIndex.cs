using System.Collections.Generic;
using System.Linq;
using SearchEngine.Database;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class DatabaseInvertedIndex: IInvertedIndex<string, Document>
    {
        private IndexingContext IndexingContext { get; set; }

        public DatabaseInvertedIndex()
        {
            IndexingContext = new IndexingContext();
        }

        public bool ContainsKey(string key)
        {
            var contains = IndexingContext.Words.Any(x => x.Statement == key);
            IndexingContext.SaveChanges();
            return contains;
        }

        public HashSet<Document> Get(string key)
        {
            Word word = IndexingContext.Words.SingleOrDefault(x => x.Statement == key);
            IndexingContext.SaveChanges();
            return word == null 
                ? new HashSet<Document>() 
                : new HashSet<Document>(word.WordDocuments.Select(x => x.Document));
        }

        public void Add(string key, Document value)
        {
            // if (!IndexingContext.Documents.Any(x => x.DocumentNumber == value.DocumentNumber))
            // {
            //     IndexingContext.Documents.Add(value);
            // }

            Word word = IndexingContext.Words.SingleOrDefault(x => x.Statement == key);
            if (word == null)
            {
                var newWord = new Word() {Statement = key};
                var pair = new Word_Document(){WordId = newWord.Statement, DocumentId = value.Id};
                IndexingContext.Words.Add(newWord);
                IndexingContext.WordDocuments.Add(pair);
            }
            else
            {
                IndexingContext.WordDocuments.Add(new Word_Document() {WordId = word.Statement, DocumentId = value.Id});
            }

            IndexingContext.SaveChanges();
        }
        

    }
}