using System;
using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IManager
    {
        static void PrintElements(ICollection<int> elements)
        {
            foreach (int id in elements)
            {
                Console.WriteLine("element" + id);
            }
        }
        public void Run();
        bool Finished(string toSearch);
    }
}