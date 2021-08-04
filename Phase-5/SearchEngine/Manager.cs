using System;
using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class Manager : IManager
    {
        private const string EndDelimiter = "$";
        
        private InvertedIndex _invertedIndex;
        private SearchEngineCore _engine;
        public void Run()
        {
            string toSearch;
            Console.WriteLine("Enter something:");
            while (!Finished(toSearch = Console.ReadLine())) {
                HashSet<int> docs = DoSearch(toSearch);
                PrintElements(docs);
                Console.WriteLine("Enter something:");
            }
        }

        public void MakeSearchEngine()
        {
            _engine = new SearchEngineCore(_invertedIndex);
        }

        public void MakeInvertedIndex(string path)
        {
            Console.WriteLine("Indexing started...");
            _invertedIndex = new InvertedIndex(new FileReader(path), new WordProcessor());
            Console.WriteLine("DONE");
        }

        public HashSet<int> DoSearch(string toSearch)
        {
            return _engine.search(toSearch);
        }

        public void PrintElements(ICollection<int> elements)
        {
            foreach (int id in elements) {
                Console.WriteLine("element" + id);
            }
        }

        public virtual bool Finished(string toSearch)
        {
            return toSearch == EndDelimiter;
        }
    }
}