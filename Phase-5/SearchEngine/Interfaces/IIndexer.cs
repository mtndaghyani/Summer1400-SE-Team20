using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IIndexer
    {
        
        List<List<string>> GetDocumentsTokens();
        IInvertedIndex<string, int> GetInvertedIndex();

        string Stem(string word);
    }
}