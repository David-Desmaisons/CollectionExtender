﻿using MoreCollection.Dictionary.Internal.Strategy;
using System.Collections.Generic;

namespace MoreCollection.Dictionary.Internal
{
    internal class MutableSingleDictionary<TKey, TValue> : SingleDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>
                        where TKey : class
    {
        private readonly IDictionaryStrategy<TKey, TValue> _DictionaryStrategy;
        internal MutableSingleDictionary(IDictionary<TKey, TValue> dictionary, IDictionaryStrategy<TKey, TValue> dictionaryStrategy)
            : base(dictionary)
        {
            _DictionaryStrategy = dictionaryStrategy;
        }

        internal MutableSingleDictionary(IDictionaryStrategy<TKey, TValue> dictionaryStrategy)
            : base()
        {
            _DictionaryStrategy = dictionaryStrategy;
        }

        private IMutableDictionary<TKey, TValue> GetNext()
        {
            return _DictionaryStrategy.GetIntermediateCollection(this);
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.AddMutable(TKey key, TValue value)
        {
            if (Count == 0)
            {
                Add(key, value);
                return this;
            }

            return GetNext().AddMutable(key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            if (Count == 0)
            {
                this[key] = value;
                return this;
            }

            if (ContainsKey(key))
            {
                this[key] = value;
                return this;
            }

            return GetNext().Update(key, value);
        }

        public IMutableDictionary<TKey, TValue> Remove(TKey key, out bool Result)
        {
            Result = Remove(key);
            return this;
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            Clear();
            return this;
        }
    }
}
