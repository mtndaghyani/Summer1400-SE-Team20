using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IIndexer <TKey, TValue>
    {
        List<List<TKey>> GetDocumentsTokens();
        IInvertedIndex<TKey, TValue> GetInvertedIndex();
        string Stem(string word);
    }
}