using System;
using System.Collections.Generic;
using SearchEngine.Classes.Core;
using SearchEngine.Classes.Indexers;
using SearchEngine.Classes.IO;
using SearchEngine.Classes.IO.Database;
using SearchEngine.Classes.IO.Database.Models;
using SearchEngine.Interfaces;
using SearchEngine.Interfaces.Indexers;

namespace SearchEngine.Classes
{
    public class Manager : IManager
    {
        private const string EndDelimiter = "$";

        private Indexer _indexer;
        private SearchEngineCore _engine;
        private readonly IndexerConfig _indexerConfig;
        private readonly DatabaseConfig _databaseConfig;

        public Manager(string indexerConfigPath, string databaseConfigPath)
        {
            _indexerConfig = JsonFileConverter.ReadConfig<IndexerConfig>(indexerConfigPath);
            _databaseConfig = JsonFileConverter.ReadConfig<DatabaseConfig>(databaseConfigPath);
            MakeInvertedIndex();
            MakeSearchEngine();
        }

        public void Run()
        {
            string toSearch;
            Console.WriteLine("Enter something:");
            while (!Finished(toSearch = Console.ReadLine()))
            {
                HashSet<Document> docs = DoSearch(toSearch);
                IManager.PrintElements(docs);
                Console.WriteLine("Enter something:");
            }
        }

        private void MakeSearchEngine()
        {
            _engine = new SearchEngineCore(new WordProcessor(), _indexer.GetInvertedIndex());
        }

        private void MakeInvertedIndex()
        {
            IInvertedIndex<string, Document> invertedIndex = _indexerConfig.IndexToDb ?
                new DatabaseInvertedIndex(_databaseConfig.DatabaseProvider) :
                new DictionaryInvertedIndex();
            _indexer = new Indexer(new FileReader(_indexerConfig.DataPath), new WordProcessor(), invertedIndex);
            if (_indexerConfig.DoIndex)
            {
                Console.WriteLine("Indexing started...");
                _indexer.SetUpInvertedIndex();
                Console.WriteLine("DONE");
            }

        }

        public virtual HashSet<Document> DoSearch(string toSearch)
        {
            return _engine.Search(toSearch);
        }
        

        public virtual bool Finished(string toSearch)
        {
            return toSearch == EndDelimiter;
        }
    }
}