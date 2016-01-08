using CollectionExtender.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Dictionary
{
    public class PolymorphDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : class 
    {
        private IMutableDictionary<TKey, TValue> _Implementation;

        public PolymorphDictionary(int TransitionToDictionary = 25)
        {
            _Implementation = new MutableSingleDictionary<TKey, TValue, MutableListDictionary<TKey, TValue>>(TransitionToDictionary);
        }

        public void Add(TKey key, TValue value)
        {
            _Implementation = _Implementation.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _Implementation = new MutableSingleDictionary<TKey, TValue, MutableListDictionary<TKey, TValue>>();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue tv = default(TValue);

            if (!_Implementation.TryGetValue(item.Key, out tv))
                return false;

            if (!Object.Equals(tv, item.Value))
                return false;

            return Remove(item.Key);
        }

        public bool Remove(TKey key)
        {
            bool res = false;
            _Implementation = _Implementation.Remove(key, out res);
            return res;
        }

        public bool ContainsKey(TKey key)
        {
            return  _Implementation.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _Implementation.Keys; }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _Implementation.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _Implementation.Values; }
        }

        public TValue this[TKey key]
        {
            get { return _Implementation[key]; }
            set { _Implementation = _Implementation.Update(key, value); }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _Implementation.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException();

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();

            _Implementation.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _Implementation.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _Implementation.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
