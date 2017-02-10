using MoreCollection.Dictionary.Internal.Strategy;
using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    public class MutableSortedDictionary<TKey, TValue> : SortedList<TKey, TValue>, IMutableDictionary<TKey, TValue>                              
    {
        private readonly IDictionaryStrategy<TKey, TValue> _DictionaryStrategy;
        public MutableSortedDictionary(IDictionaryStrategy<TKey, TValue> dictionaryStrategy)
        {
            _DictionaryStrategy = dictionaryStrategy;
        }

        public MutableSortedDictionary(IDictionary<TKey, TValue> collection, IDictionaryStrategy<TKey, TValue> dictionaryStrategy)
            : base(collection)
        {
            _DictionaryStrategy = dictionaryStrategy;
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.AddMutable(TKey key, TValue value)
        {
            return _DictionaryStrategy.Add(this, key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            return _DictionaryStrategy.Update(this, key, value);
        }

        public IMutableDictionary<TKey,TValue> Remove(TKey key, out bool Result)
        {
            return _DictionaryStrategy.Remove(this, key, out Result);
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            return _DictionaryStrategy.GetEmpty();
        }
    }
}
