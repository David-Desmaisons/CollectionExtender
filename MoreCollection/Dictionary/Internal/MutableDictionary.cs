using MoreCollection.Dictionary.Internal.Strategy;
using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    internal class MutableDictionary<TKey, TValue>: Dictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>
    {
        internal MutableDictionary(): base()
        {
        }

        internal MutableDictionary(IDictionary<TKey, TValue> collection) : base(collection)
        {
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.AddMutable(TKey key, TValue value)
        {
 	        Add(key, value);
            return this;
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            this[key] = value;
            return this;
        }

        public IMutableDictionary<TKey,TValue> Remove(TKey key, out bool Result)
        {
            Result = Remove(key);
            return Result ? DictionaryStrategyFactory<TKey>.Strategy.CheckDictionaryRemoved(this) : this;
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            return DictionaryStrategyFactory<TKey>.Strategy.GetEmpty<TKey,TValue>();
        }
    }
}
