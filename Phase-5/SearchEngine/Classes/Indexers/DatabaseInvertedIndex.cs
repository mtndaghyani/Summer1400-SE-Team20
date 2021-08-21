using System;
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
        private const string ConnectionString = "Server=.; Database=IndexingDB; Trusted_Connection=True;";
        private IndexingContext IndexingContext { get; set; }

        public DatabaseInvertedIndex(string databaseProvider)
        {
            DbContextOptionsBuilder<IndexingContext> contextOptionsBuilder = new();
            if(databaseProvider == "SQLServer")
                contextOptionsBuilder.UseSqlServer(ConnectionString);
            else if (databaseProvider == "InMemory")
                contextOptionsBuilder.UseInMemoryDatabase("MyInMemoryDB");
            else
                throw new Exception("Invalid DatabaseProvider");
            IndexingContext = new IndexingContext(contextOptionsBuilder.Options);
        }

        public bool ContainsKey(string key)
        {
            var contains = IndexingContext.Words.Any(x => x.Statement == key);
            return contains;
        }

        public HashSet<Document> Get(string key)
        {
            Word word = IndexingContext.Words
                .Include(x => x.WordDocuments)
                .ThenInclude(y => y.Document)
                .SingleOrDefault(x => x.Statement == key);
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
                var pair = new WordDocument(){Word = newWord, Document = value};
                IndexingContext.Words.Add(newWord);
                IndexingContext.WordDocuments.Add(pair);
            }
            else
            {
                IndexingContext.WordDocuments.Add(new WordDocument() {Word = word, Document = value});
            }

            IndexingContext.SaveChanges();
        }
        

    }
}