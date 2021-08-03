using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IManager
    {
        public void Run();
        void MakeSearchEngine();
        void MakeInvertedIndex(string path);
        HashSet<int> DoSearch(string toSearch);
        void PrintElements(ICollection<int> elements);
        bool Finished(string toSearch);
    }
}