using CollectionExtender.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionExtender.Dictionary.Internal
{
    internal class MutableSingleDictionary<TKey, TValue> :
                        SingleDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>
                        where TKey : class
    {
        private readonly int _Transition;
        private readonly Type _TargetType;
        internal MutableSingleDictionary( IDictionary<TKey, TValue> dictionary, Type targetType, int transition=10)
            : base(dictionary)
        {
            _Transition = transition;
            _TargetType = targetType;
        }

        internal MutableSingleDictionary(Type targetType, int transition = 10) : base()
        {
            _Transition = transition;
            _TargetType = targetType;
        }

        private IMutableDictionary<TKey, TValue> GetNext()
        {
            return Introspector.BuildInstance<IMutableDictionary<TKey, TValue>>(_TargetType, this, 10);
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
    }
}
