﻿using System;
using System.Collections.Generic;
using SearchEngine.Classes.Core;
using SearchEngine.Classes.Indexers;
using SearchEngine.Classes.IO;
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
        private readonly Config _config;

        public Manager(string configPath)
        {
            _config = Config.ReadConfig(configPath);
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
            IInvertedIndex<string, Document> invertedIndex = _config.IndexFromDb ?
                new DatabaseInvertedIndex(_config.DatabaseProvider) :
                new DictionaryInvertedIndex();
            _indexer = new Indexer(new FileReader(_config.DataPath), new WordProcessor(), invertedIndex);
            if (_config.DoIndex)
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