using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Dictionary.Internal
{
    internal class MutableSortedDictionary<TKey, TValue> : SortedDictionary<TKey, TValue>, 
                        IMutableDictionary<TKey, TValue> where TKey : class                              
    {
        private readonly DictionarySwitcher<TKey, TValue> _DictionarySwitcher;
        public MutableSortedDictionary(int limit=10):base()
        {
            _DictionarySwitcher = new DictionarySwitcher<TKey, TValue>(this, limit);
        }

        public MutableSortedDictionary(IDictionary<TKey, TValue> collection, int limit = 10)
            : base(collection)
        {
            _DictionarySwitcher = new DictionarySwitcher<TKey, TValue>(this, limit);
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.AddMutable(TKey key, TValue value)
        {
            return _DictionarySwitcher.Add(key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            return _DictionarySwitcher.Update(key, value);
        }

        public IMutableDictionary<TKey,TValue> Remove(TKey key, out bool Result)
        {
            return _DictionarySwitcher.Remove(key, out Result);
        }
    }
}
