using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Dictionary.Internal
{
    internal class DictionarySwitcher<TKey, TValue>  where TKey : class
    {
        private readonly int _TransitionToDictionary;
        private readonly IMutableDictionary<TKey, TValue> _Current;
        public DictionarySwitcher (IMutableDictionary<TKey, TValue> current, int limit = 10)
        {
            _Current = current;
            _TransitionToDictionary = limit;
        }

        private IMutableDictionary<TKey, TValue> GetNext()
        {
            return new MutableDictionary<TKey, TValue> (_Current, _Current.GetType(), _TransitionToDictionary);
        }

        internal IMutableDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            if (_Current.Count < _TransitionToDictionary)
            {
                _Current.Add(key, value);
                return _Current;
            }

            return GetNext().AddMutable(key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            if ((_Current.Count == _TransitionToDictionary) && (!_Current.ContainsKey(key)))
            {
                return GetNext().AddMutable(key, value);
            }

            _Current[key] = value;
            return _Current;
        }

        public IMutableDictionary<TKey, TValue> Remove(TKey key, out bool Result)
        {
            Result = _Current.Remove(key);
            
            if (_Current.Count == 1)
            {
                return new MutableSingleDictionary<TKey, TValue>(_Current, 
                                        _Current.GetType(), _TransitionToDictionary);
            }

            return _Current;
        }
    }
}
