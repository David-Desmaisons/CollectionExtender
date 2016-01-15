using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Dictionary.Internal
{
    public interface IMutableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        IMutableDictionary<TKey, TValue> AddMutable(TKey key, TValue value);

        IMutableDictionary<TKey, TValue> Update(TKey key, TValue value);

        IMutableDictionary<TKey, TValue> Remove(TKey key, out bool Result);
    }
}
