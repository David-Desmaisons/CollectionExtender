using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollection.Set.Infra
{
    internal class ListSet<T> : ILetterSimpleSet<T>
    {
        private readonly object[] _Items;
        private int _Count = 0;
        private readonly ILetterSimpleSetFactory _Factory;

        public int Count => _Count;

        internal ListSet(ILetterSimpleSetFactory factory, int maxItem)
        {
            _Factory = factory;
            _Items = new object[maxItem];
        }

        internal ListSet(ILetterSimpleSetFactory factory, T item, int maxItem): this (factory, maxItem)
        {
            _Items[0] = item;
            _Count = 1;
        }

        internal ListSet(ILetterSimpleSetFactory factory, HashSet<T> items, int maxItem)
        {
            _Factory = factory;
            var count = items.Count;
            if (count >= maxItem)
            {
                throw new ArgumentOutOfRangeException($"items count ({count}) >= Max ({maxItem})");
            }

            _Items = new object[maxItem];

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
                if (Object.Equals(item, _Items[i]))
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

        private IEnumerable<T> GetEnumerable()
        {
            return _Items.TakeWhile(t => t != null).Cast<T>();
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
