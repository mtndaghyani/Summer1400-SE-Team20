using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IIndexer
    {
        
        List<List<string>> GetDocumentsTokens();
        Dictionary<string, HashSet<int>> GetInvertedIndex();

        string Stem(string word);
    }
}