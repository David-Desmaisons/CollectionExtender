using System;
using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;
using MoreCollection.Dictionary;

namespace MoreCollection.Composed
{
    public class SimpleLookUp<TKey, TElement> : ILookup<TKey, TElement> where TKey : class
    {
        private IDictionary<TKey, List<TElement>> _LookUpDictionary;

        public SimpleLookUp(Func<IDictionary<TKey, List<TElement>>> Factory = null)
        {
            _LookUpDictionary = (Factory != null) ? Factory() : new Dictionary<TKey, List<TElement>>();
        }

        public static IDictionary<TKey, List<TElement>> Hybrid()
        {
            return new HybridDictionary<TKey, List<TElement>>();
        }

        public void Add(TKey key, TElement element)
        {
            _LookUpDictionary.GetOrAddEntity(key, (k) => new List<TElement>()).Add(element);
        }

        public bool Remove(TKey key, TElement element)
        {
            List<TElement> list;
            if (!_LookUpDictionary.TryGetValue(key, out list))
                return false;

            var res = list.Remove(element);
            if (list.Count == 0)
                _LookUpDictionary.Remove(key);

            return res;
        }

        public bool Contains(TKey key)
        {
            return _LookUpDictionary.ContainsKey(key);
        }

        public int Count
        {
            get { return _LookUpDictionary.Count; }
        }

        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                List<TElement> res = null;
                if (_LookUpDictionary.TryGetValue(key, out res))
                    return res;

                throw new KeyNotFoundException();
            }
        }

        #region grouping

        private class Grouping<TKey2, TElement2> : IGrouping<TKey2, TElement2>
        {
            private TKey2 _Key;
            private IEnumerable<TElement2> _Elements;

            internal Grouping(TKey2 ikey, IEnumerable<TElement2> ielems)
            {
                _Key = ikey;
                _Elements = ielems;
            }

            public TKey2 Key => _Key;

            public System.Collections.Generic.IEnumerator<TElement2> GetEnumerator()
            {
                return _Elements.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _Elements.GetEnumerator();
            }
        }

        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            foreach (var keyValue in _LookUpDictionary)
            {
                yield return new Grouping<TKey, TElement>(keyValue.Key, keyValue.Value);
            }
        }

        #endregion

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
