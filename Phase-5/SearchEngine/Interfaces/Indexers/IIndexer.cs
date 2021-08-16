using System.Collections.Generic;

namespace SearchEngine.Interfaces.Indexers
{
    public interface IIndexer <TKey, TValue>
    {
        List<List<TKey>> GetDocumentsTokens();
        IInvertedIndex<TKey, TValue> GetInvertedIndex();
    }
}