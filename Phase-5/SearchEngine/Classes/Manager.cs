using System;
using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class Manager : IManager
    {
        private const string EndDelimiter = "$";

        private InvertedIndex _invertedIndex;
        private SearchEngineCore _engine;

        public Manager(string path)
        {
            MakeInvertedIndex(path);
            MakeSearchEngine();
        }

        public void Run()
        {
            string toSearch;
            Console.WriteLine("Enter something:");
            while (!Finished(toSearch = Console.ReadLine()))
            {
                HashSet<int> docs = DoSearch(toSearch);
                IManager.PrintElements(docs);
                Console.WriteLine("Enter something:");
            }
        }

        private void MakeSearchEngine()
        {
            _engine = new SearchEngineCore(_invertedIndex);
        }

        private void MakeInvertedIndex(string path)
        {
            Console.WriteLine("Indexing started...");
            _invertedIndex = new InvertedIndex(new FileReader(path), new WordProcessor());
            Console.WriteLine("DONE");
        }

        public virtual HashSet<int> DoSearch(string toSearch)
        {
            return _engine.search(toSearch);
        }
        

        public virtual bool Finished(string toSearch)
        {
            return toSearch == EndDelimiter;
        }
    }
}