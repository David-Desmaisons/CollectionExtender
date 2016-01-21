using MoreCollection.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Dictionary.Internal
{
    internal class SingleDictionary<Tkey, Tvalue> : IDictionary<Tkey, Tvalue> where Tkey: class
    {
        private Tkey _Key;
        private Tvalue _Value;
        internal SingleDictionary(IDictionary<Tkey, Tvalue> dictionary):this()
        {
            var count = dictionary.Count();

            if (count ==1)
            {
                var keyvaluepair = dictionary.First();
                _Key = keyvaluepair.Key;
                _Value = keyvaluepair.Value;
                return;
            }

            if (count != 0)
                throw new ArgumentOutOfRangeException("dictionary", "dictionary should have only 0 or 1 element");
        }

        internal SingleDictionary()
        {
            _Key = null;
            _Value = default(Tvalue);
        }

        public void Add(KeyValuePair<Tkey, Tvalue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(Tkey key, Tvalue value)
        {
            if (_Key!=null)
                throw new NotImplementedException();

            if (key==null)
                throw new ArgumentNullException();

            _Key = key;
            _Value = value;
        }

        public bool Remove(Tkey key)
        {
            if (key == null)
                throw new ArgumentNullException();

            if (_Key == null)
                return false;

            if (Object.ReferenceEquals(_Key,key))
            {
                _Key = null;
                return true;
            }

            return false;
        }

        public void Clear()
        {
            _Key = null;
        }

        public bool Remove(KeyValuePair<Tkey, Tvalue> item)
        {
            if ((Object.Equals(item.Key, _Key)) && (Object.Equals(item.Value, _Value)))
            {
                _Key = null;
                return true;
            }
            return false;
        }

        public bool ContainsKey(Tkey key)
        {
            return Object.Equals(key, _Key);
        }

        public bool TryGetValue(Tkey key, out Tvalue value)
        {
            var ok = (_Key!=null) && Object.Equals(key, _Key);
            value = ok ? _Value : default(Tvalue);
            return ok;
        }

        public ICollection<Tvalue> Values
        {
            get
            { 
                var res = new List<Tvalue>(); 
                if (_Key != null) 
                    res.Add(_Value); 
                return res; 
            }
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
                if (key == null)
                    throw new ArgumentNullException();

                if (_Key==null)
                {
                    _Key = key;
                    _Value = value;
                    return;
                }

                if (Object.Equals(key, _Key))
                {
                    _Value = value;
                }
                else throw new NotImplementedException();
            }
        }

        public ICollection<Tkey> Keys
        {
            get
            { 
                var res = new List<Tkey>(); 
                if (_Key != null)
                    res.Add(_Key); 
                return res; 
            }
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            if (_Key!=null)
                yield return new KeyValuePair<Tkey, Tvalue>(_Key, _Value);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(KeyValuePair<Tkey, Tvalue> item)
        {
            return (Object.Equals(item.Key, _Key)) && (Object.Equals(item.Value, _Value));
        }

        public void CopyTo(KeyValuePair<Tkey, Tvalue>[] array, int arrayIndex)
        {
            EnumerableHelper.CopyTo(this, array, arrayIndex);
        }

        public int Count
        {
            get { return (_Key == null) ? 0 : 1; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}
