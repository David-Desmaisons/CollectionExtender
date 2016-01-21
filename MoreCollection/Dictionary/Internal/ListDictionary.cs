using System;
using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;

namespace MoreCollection.Dictionary.Internal
{
    internal class ListDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>> _List;

        public ListDictionary()
        {
            _List = new List<KeyValuePair<TKey, TValue>>();
        }

        public ListDictionary(IDictionary<TKey, TValue> from)
        {
            _List = new List<KeyValuePair<TKey, TValue>>();
            _List.AddRange(from);
        }

        private int GetIndex(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return _List.Index(kvp => kvp.Key.Equals(key));
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key", "ArgumentNull_Key");
            }

            if (ContainsKey(key))
                throw new ArgumentException("Argument_AddingDuplicate");

            _List.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            return _List.Select(tkv => tkv.Key).Any(k => k.Equals(key));
        }

        public ICollection<TKey> Keys
        {
            get { return _List.Select(tkv => tkv.Key).ToList(); }
        }

        public bool Remove(TKey key)
        {
            var index = GetIndex(key);
            if (index == -1)
                return false;

            _List.RemoveAt(index);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var index = GetIndex(key);
            if (index == -1)
            {
                value = default(TValue);
                return false;
            }

            value = _List[index].Value;
            return true;
        }

        public ICollection<TValue> Values
        {
            get { return _List.Select(kv => kv.Value).ToList(); }
        }

        public TValue this[TKey key]
        {
            get
            {
                try
                {
                    return _List[GetIndex(key)].Value;
                }
                catch
                {
                    throw new KeyNotFoundException();
                }
            }
            set
            {
                Remove(key);
                _List.Add(new KeyValuePair<TKey, TValue>(key, value));
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (ContainsKey(item.Key))
                throw new ArgumentException("Argument_AddingDuplicate");

            _List.Add(item);
        }

        public void Clear()
        {
            _List.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var index = GetIndex(item.Key);
            return (index != -1) && Object.Equals(_List[index].Value, item.Value);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var index = GetIndex(item.Key);
            if (index == -1)
            {
                return false;
            }

            var found = _List[index];
            if (!Object.Equals(item.Value, found.Value))
                return false;

            _List.RemoveAt(index);
            return true;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
