﻿using MoreCollection.Dictionary.Internal.Strategy;
using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    internal class MutableDictionary<TKey, TValue>: Dictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>
    {
        private readonly IDictionaryStrategy _DictionaryStrategy;

        internal MutableDictionary(IDictionaryStrategy dictionaryStrategy): base()
        {
            _DictionaryStrategy = dictionaryStrategy;
        }

        internal MutableDictionary(IDictionary<TKey, TValue> collection, IDictionaryStrategy dictionaryStrategy)
            : base(collection)
        {
            _DictionaryStrategy = dictionaryStrategy;
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
            return Result ? _DictionaryStrategy.CheckDictionaryRemoved(this) : this;
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            return _DictionaryStrategy.GetEmpty<TKey,TValue>();
        }
    }
}
