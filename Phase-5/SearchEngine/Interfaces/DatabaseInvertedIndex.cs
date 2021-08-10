using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IInvertedIndex<TKey, TValue>
    {
        bool ContainsKey(TKey key);
        HashSet<TValue> Get(TKey key);
    }
}