using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Dictionary.Internal
{
    internal class MutableListDictionary<TKey, TValue> : ListDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue>
                    where TKey : class                              
    {
        private readonly int _TransitionToDictionary;
        public MutableListDictionary(int limit=10):base()
        {
            _TransitionToDictionary = limit;
        }

        public MutableListDictionary(IDictionary<TKey, TValue> collection, int limit = 10)
            : base(collection)
        {
            _TransitionToDictionary = limit;
        }

        private IMutableDictionary<TKey, TValue> GetNext()
        {
            return new MutableDictionary<TKey, TValue, MutableListDictionary<TKey, TValue>>
                                (this, _TransitionToDictionary);
        }

        IMutableDictionary<TKey,TValue> IMutableDictionary<TKey,TValue>.Add(TKey key, TValue value)
        {
            if (Count<_TransitionToDictionary)
            {
                Add(key, value);
                return this;
            }
 	       
            return GetNext().Add(key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            if ( (Count == _TransitionToDictionary) && (!ContainsKey(key)))
            {
                return GetNext().Add(key, value);
            }

            this[key] = value;
            return this;
        }

        public IMutableDictionary<TKey,TValue> Remove(TKey key, out bool Result)
        {
            Result = false;
            if (Count == 2)
            {
                Result = Remove(key);
                if (!Result)
                    return this;

                var keyvaluepair = this.First();
                return new MutableSingleDictionary<TKey, TValue, MutableListDictionary<TKey, TValue>>
                    (keyvaluepair.Key, keyvaluepair.Value);
            }

            Result = Remove(key);
            return this;
        }
    }
}
