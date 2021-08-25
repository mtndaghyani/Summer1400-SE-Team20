using System.Collections.Generic;

namespace SearchEngine.Interfaces.Indexers
{
    public interface IIndexer <TKey, TValue>
    {
        IEnumerable<List<TKey>> GetDocumentsTokens();
        IInvertedIndex<TKey, TValue> GetInvertedIndex();
    }
}