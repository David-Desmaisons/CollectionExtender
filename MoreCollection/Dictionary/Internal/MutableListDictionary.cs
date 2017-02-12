using MoreCollection.Dictionary.Internal.Strategy;
using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    public class MutableListDictionary<TKey, TValue> : ListDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>                              
    {
        private readonly IDictionaryStrategy _DictionaryStrategy;
        public MutableListDictionary(IDictionaryStrategy dictionaryStrategy)
        {
            _DictionaryStrategy = dictionaryStrategy;
        }

        public MutableListDictionary(IDictionary<TKey, TValue> collection, IDictionaryStrategy dictionaryStrategy) : base(collection)
        {
            _DictionaryStrategy = dictionaryStrategy;
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.AddMutable(TKey key, TValue value)
        {
            return _DictionaryStrategy.Add(this, key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            return _DictionaryStrategy.Update(this,  key, value);
        }

        public IMutableDictionary<TKey,TValue> Remove(TKey key, out bool result)
        {
            return _DictionaryStrategy.Remove(this,  key, out result);
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            return _DictionaryStrategy.GetEmpty<TKey, TValue>();
        }
    }
}
