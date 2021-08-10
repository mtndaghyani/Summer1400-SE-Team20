using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IIndexer
    {
        
        List<List<string>> GetDocumentsTokens();
        Dictionary<string, HashSet<int>> GetInvertedIndex();

        public string Stem(string word);
    }
}