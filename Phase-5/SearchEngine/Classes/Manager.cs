using System;
using System.Collections.Generic;
using SearchEngine.Database;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class Manager : IManager
    {
        private const string EndDelimiter = "$";

        private Indexer _indexer;
        private SearchEngineCore _engine;
        private readonly Config _config;

        public Manager(string configPath)
        {
            _config = Config.ReadConfig(configPath);
            MakeInvertedIndex(_config.DoIndex);
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
            _engine = new SearchEngineCore(_indexer);
        }

        private void MakeInvertedIndex(bool doIndex)
        {
            IInvertedIndex<string, Document> invertedIndex = _config.IndexFromDb ?
                new DatabaseInvertedIndex() :
                new DictionaryInvertedIndex();
            _indexer = new Indexer(new FileReader(_config.DataPath), new WordProcessor(), invertedIndex);
            if (doIndex)
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