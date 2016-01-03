using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Dictionary.Internal
{
    internal class SingleDictionary<Tkey, Tvalue> : IDictionary<Tkey, Tvalue>
    {
        private Tkey _Key;
        private Tvalue _Value;
        internal SingleDictionary(Tkey key, Tvalue value)
        {
            _Key = key;
            _Value = value;
        }

        #region Not Implemented

        public void Add(KeyValuePair<Tkey, Tvalue> item)
        {
            throw new NotImplementedException();
        }

        public void Add(Tkey key, Tvalue value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Tkey key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Tkey, Tvalue> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region readonly

        public bool ContainsKey(Tkey key)
        {
            return Object.Equals(key, _Key);
        }

        public bool TryGetValue(Tkey key, out Tvalue value)
        {
            bool ok = Object.Equals(key, _Key);
            value = ok ? _Value : default(Tvalue);
            return ok;
        }

        public ICollection<Tvalue> Values
        {
            get { var res = new List<Tvalue>(); res.Add(_Value); return res; }
        }

        public Tvalue this[Tkey key]
        {
            get
            {
                if (Object.Equals(key, _Key))
                    return _Value;

                throw new KeyNotFoundException();
            }
            set
            {
                if (Object.Equals(key, _Key))
                {
                    _Value = value;
                }
                else throw new NotImplementedException();
            }
        }

        public ICollection<Tkey> Keys
        {
            get { var res = new List<Tkey>(); res.Add(_Key); return res; }
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            yield return new KeyValuePair<Tkey, Tvalue>(_Key, _Value);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            yield return new KeyValuePair<Tkey, Tvalue>(_Key, _Value);
        }

        public bool Contains(KeyValuePair<Tkey, Tvalue> item)
        {
            return (Object.Equals(item.Key, _Key)) && (Object.Equals(item.Value, _Value));
        }

        public void CopyTo(KeyValuePair<Tkey, Tvalue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException();

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();

            array[arrayIndex] = new KeyValuePair<Tkey, Tvalue>(_Key, _Value);
        }

        public int Count
        {
            get { return 1; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        #endregion
    }
}
