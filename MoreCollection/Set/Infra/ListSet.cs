using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollection.Set.Infra
{
    internal class ListSet<T> : ILetterSimpleSet<T> where T : class
    {
        private T[] _Items;
        private int _Count = 0;
        private readonly ILetterSimpleSetFactory<T> _Factory;

        internal ListSet(ILetterSimpleSetFactory<T> Factory, int MaxItem)
        {
            _Factory = Factory;
            _Items = new T[MaxItem];
        }

        internal ListSet(ILetterSimpleSetFactory<T> Factory, T item, int MaxItem)
        {
            _Factory = Factory;
            _Items = new T[MaxItem];
            _Items[0] = item;
            _Count = 1;
        }

        internal ListSet(ILetterSimpleSetFactory<T> Factory, HashSet<T> items, int MaxItem)
        {
            _Factory = Factory;
            int count = items.Count();
            if (count >= MaxItem)
            {
                throw new ArgumentOutOfRangeException(
                                string.Format("items count ({0}) >= Max ({1})", count, MaxItem));
            }

            _Items = new T[MaxItem];

            int index = 0;
            foreach (T item in items)
            {
                _Items[index++] = item;
            }

            _Count = count;
        }

        private bool Add(T item)
        {
            if (Contains(item))
                return false;

            _Items[_Count++] = item;
            return true;
        }

        private bool Remove(T item)
        {
            if (item == null)
                return false;

            for (int i = 0; i < _Count; i++)
            {
                T iitem = _Items[i];
                if (Object.Equals(item, iitem))
                {
                    _Items[i] = _Items[_Count - 1];
                    _Items[_Count - 1] = null;
                    _Count--;
                    return true;
                }
            }

            return false;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _Count; i++)
            {
                if (Object.Equals( item, _Items[i]))
                    return true;
            }
            return false;
        }

        public int Count
        {
            get { return _Count; }
        }

        private IEnumerable<T> GetEnumerable()
        {
            return _Items.TakeWhile(t => t != null);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ILetterSimpleSet<T> Add(T item, out bool success)
        {
            success = Add(item);

            return success ? _Factory.OnAdd(this) : this;
        }

        public ILetterSimpleSet<T> Remove(T item, out bool success)
        {
            success = Remove(item);
            return this;
        }
    }
}
