using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    public interface IMutableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        IMutableDictionary<TKey, TValue> AddMutable(TKey key, TValue value);

        IMutableDictionary<TKey, TValue> Update(TKey key, TValue value);

        IMutableDictionary<TKey, TValue> Remove(TKey key, out bool Result);

        IMutableDictionary<TKey, TValue> ClearMutable();
    }
}
