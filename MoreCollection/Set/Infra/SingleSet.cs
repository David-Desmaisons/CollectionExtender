using System;
using System.Collections.Generic;

namespace MoreCollection.Set.Infra
{
    public class SingleSet<T> : ILetterSimpleSet<T>
    {
        private object _SingleItem = null;
        private readonly ILetterSimpleSetFactory _Factory;

        public int Count => (_SingleItem != null) ? 1 : 0;

        internal SingleSet(ILetterSimpleSetFactory factory)
        {
            _Factory = factory;
        }

        internal SingleSet(ILetterSimpleSetFactory factory, T item): this(factory)
        {
            Add(item);
        }

        private Nullable<bool> Add(T item)
        {
            if (item == null)
                return false;

            if (_SingleItem == null)
            {
                _SingleItem = item;
                return true;
            }
                
            return (Object.Equals(item, _SingleItem)) ? false : new bool?();
        }

        private bool Remove(T item)
        {
            if (Object.Equals(_SingleItem,item))
            {
                _SingleItem = null;
                return true;
            }

            return false;
        }

        public bool Contains(T item)
        {
            return ((_SingleItem!=null) &&  (Object.Equals(item, _SingleItem)));
        }

        private IEnumerable<T> GetEnumerable()
        {
            if (_SingleItem!=null)
                yield return (T)_SingleItem;
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
            var res = Add(item);
            if (res != null)
            {
                success = res.Value;
                return this;
            }

            success = true;
            return _Factory.GetDefault((T)_SingleItem, item);
        }

        public ILetterSimpleSet<T> Remove(T item, out bool success)
        {
            success = Remove(item);
            return this;
        }
    }
}
