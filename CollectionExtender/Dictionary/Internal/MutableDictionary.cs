using CollectionExtender.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Dictionary.Internal
{
    internal class MutableDictionary<TKey, TValue, TDicionary> : 
                        Dictionary<TKey, TValue>, IMutableDictionary<TKey, TValue> 
                        where TDicionary : class, IMutableDictionary<TKey, TValue>
    {
        private readonly int _TransitionToList;
        internal MutableDictionary(int limit=10):base()
        {
            _TransitionToList = limit;
        }

        internal MutableDictionary(IDictionary<TKey, TValue> collection,int limit=10):base(collection)
        {
            _TransitionToList = limit;
        }

        IMutableDictionary<TKey,TValue> IMutableDictionary<TKey,TValue>.Add(TKey key, TValue value)
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
                return Introspector.Build<TDicionary>(this, _TransitionToList);
            }

            Result = Remove(key);
            return this;
        }
    }
}
