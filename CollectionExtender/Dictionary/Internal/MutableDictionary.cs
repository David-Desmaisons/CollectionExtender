using MoreCollection.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Dictionary.Internal
{
    internal class MutableDictionary<TKey, TValue>: Dictionary<TKey, TValue>,
        IMutableDictionary<TKey, TValue> 
    {
        private readonly int _TransitionToList;
        private readonly Type _TargetType;

        internal MutableDictionary(Type targetType, int limit = 10)
            : base()
        {
            _TransitionToList = limit;
            _TargetType = targetType;
        }

        internal MutableDictionary(IDictionary<TKey, TValue> collection, Type targetType, int limit = 10)
            : base(collection)
        {
            _TransitionToList = limit;
            _TargetType = targetType;
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
            Result = false;
            if (Count == _TransitionToList + 1)
            {
                if (!Remove(key))
                {
                    return this;
                }
                Result = true;
                return Introspector.BuildInstance<IMutableDictionary<TKey, TValue>>(_TargetType, this, _TransitionToList);
            }

            Result = Remove(key);
            return this;
        }
    }
}
