using MoreCollection.Dictionary.Internal.Strategy;
using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    public class MutableListDictionary<TKey, TValue> : ListDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>                              
    {
        public MutableListDictionary()
        {
        }

        public MutableListDictionary(IDictionary<TKey, TValue> collection) : base(collection)
        {
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.AddMutable(TKey key, TValue value)
        {
            return DictionaryStrategyFactory<TKey>.Strategy.Add(this, key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            return DictionaryStrategyFactory<TKey>.Strategy.Update(this,  key, value);
        }

        public IMutableDictionary<TKey,TValue> Remove(TKey key, out bool result)
        {
            return DictionaryStrategyFactory<TKey>.Strategy.Remove(this,  key, out result);
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            return DictionaryStrategyFactory<TKey>.Strategy.GetEmpty<TKey, TValue>();
        }
    }
}
