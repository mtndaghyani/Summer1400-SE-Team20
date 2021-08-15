using System.Collections.Generic;

namespace SearchEngine.Interfaces.Indexers
{
    public interface IInvertedIndex<TKey, TValue>
    {
        bool ContainsKey(TKey key);
        HashSet<TValue> Get(TKey key);
        void Add(TKey key, TValue value);
    }
}