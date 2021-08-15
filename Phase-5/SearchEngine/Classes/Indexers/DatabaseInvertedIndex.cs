using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Classes.IO.Database;
using SearchEngine.Classes.IO.Database.Models;
using SearchEngine.Interfaces.Indexers;

namespace SearchEngine.Classes.Indexers
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
            Word word = IndexingContext.Words
                .Include(x => x.WordDocuments)
                .ThenInclude(y => y.Document)
                .SingleOrDefault(x => x.Statement == key);
            IndexingContext.SaveChanges();
            return word == null 
                ? new HashSet<Document>() 
                : new HashSet<Document>(word.WordDocuments.Select(x => x.Document));
        }

        public void Add(string key, Document value)
        {
            Word word = IndexingContext.Words.SingleOrDefault(x => x.Statement == key);
            if (word == null)
            {
                var newWord = new Word() {Statement = key};
                var pair = new Word_Document(){Word = newWord, Document = value};
                IndexingContext.Words.Add(newWord);
                IndexingContext.WordDocuments.Add(pair);
            }
            else
            {
                IndexingContext.WordDocuments.Add(new Word_Document() {Word = word, Document = value});
            }

            IndexingContext.SaveChanges();
        }
        

    }
}