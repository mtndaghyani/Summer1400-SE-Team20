using System;
using System.Collections.Generic;
using SearchEngine.Database;

namespace SearchEngine.Interfaces
{
    public interface IManager
    {
        static void PrintElements(ICollection<Document> elements)
        {
            foreach (Document doc in elements)
            {
                Console.WriteLine("element" + doc.DocumentIdentification);
            }
        }
        void Run();
        bool Finished(string toSearch);
    }
}