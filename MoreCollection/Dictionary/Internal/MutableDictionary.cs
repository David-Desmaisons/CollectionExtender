using MoreCollection.Dictionary.Internal.Strategy;
using MoreCollection.Infra;
using System;
using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    internal class MutableDictionary<TKey, TValue>: Dictionary<TKey, TValue>, IMutableDictionary<TKey, TValue> where TKey:class 
    {
        private readonly IDictionaryStrategy<TKey, TValue> _DictionarySwitcher;

        internal MutableDictionary(IDictionaryStrategy<TKey, TValue> switcher): base()
        {
            _DictionarySwitcher = switcher;
        }

        internal MutableDictionary(IDictionary<TKey, TValue> collection, IDictionaryStrategy<TKey, TValue> switcher)
            : base(collection)
        {
            _DictionarySwitcher = switcher;
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
            return Result ? _DictionarySwitcher.CheckDictionaryRemoved(this) : this;
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            return _DictionarySwitcher.GetEmpty();
        }
    }
}
