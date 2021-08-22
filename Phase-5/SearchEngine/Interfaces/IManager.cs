using System;
using System.Collections.Generic;
using SearchEngine.Classes.IO.Database.Models;

namespace SearchEngine.Interfaces
{
    public interface IManager
    {
        static void PrintElements(ICollection<Document> elements)
        {
            foreach (Document doc in elements)
            {
                Console.WriteLine("element" + doc.DocumentNumber);
            }
        }
        void Run();
        bool Finished(string toSearch);
    }
}