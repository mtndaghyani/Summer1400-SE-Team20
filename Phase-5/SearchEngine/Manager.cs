using System;
using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class Manager : IManager
    {
        private const string EndDelimiter = "$";
        public void Run()
        {
            String toSearch;
            Console.WriteLine("Enter something:");
            while (!Finished(toSearch = Console.ReadLine())) {
                HashSet<int> docs = DoSearch(toSearch);
                PrintElements(docs);
                Console.WriteLine("Enter something:");
            }
        }

        public void MakeSearchEngine()
        {
            throw new System.NotImplementedException();
        }

        public void MakeInvertedIndex(string path)
        {
            throw new System.NotImplementedException();
        }

        public HashSet<int> DoSearch(string toSearch)
        {
            throw new System.NotImplementedException();
        }

        public void PrintElements(ICollection<int> elements)
        {
            foreach (int id in elements) {
                Console.WriteLine("element" + id);
            }
        }

        public bool Finished(string toSearch)
        {
            return toSearch == EndDelimiter;
        }
    }
}