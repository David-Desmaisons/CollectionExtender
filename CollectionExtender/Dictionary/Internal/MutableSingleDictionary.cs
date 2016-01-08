using CollectionExtender.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionExtender.Dictionary.Internal
{
    internal class MutableSingleDictionary<TKey, TValue, TDicionary> :
                        SingleDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>
                        where TDicionary : class, IMutableDictionary<TKey, TValue>
                        where TKey : class
    {
        private readonly int _Transition;
        internal MutableSingleDictionary( IDictionary<TKey, TValue> dictionary, int transition=10)
            : base(dictionary)
        {
            _Transition = transition;
        }

        internal MutableSingleDictionary(int transition = 10) : base()
        {
            _Transition = transition;
        }

        private IMutableDictionary<TKey, TValue> GetNext()
        {
            return Introspector.Build<TDicionary>(this, 10);
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            if (Count == 0)
            {
                Add(key, value);
                return this;
            }

            return GetNext().Add(key, value);
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
    }
}
