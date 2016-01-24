using MoreCollection.Dictionary.Internal.Strategy;
using MoreCollection.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoreCollection.Dictionary.Internal
{
    internal class MutableSingleDictionary<TKey, TValue> : SingleDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>
                        where TKey : class
    {
        private readonly IDictionaryStrategy<TKey, TValue> _Switcher;
        internal MutableSingleDictionary( IDictionary<TKey, TValue> dictionary, IDictionaryStrategy<TKey, TValue> switcher )
            : base(dictionary)
        {
            _Switcher = switcher;
        }

        internal MutableSingleDictionary(IDictionaryStrategy<TKey, TValue> switcher) : base()
        {
            _Switcher = switcher;
        }

        private IMutableDictionary<TKey, TValue> GetNext()
        {
            return _Switcher.GetIntermediateCollection(this);
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
