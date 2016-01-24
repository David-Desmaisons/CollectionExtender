using System;
using System.Collections.Generic;

namespace MoreCollection.Set.Infra
{
    public class SingleSet<T> : ILetterSimpleSet<T> where T : class
    {
        private T _SingleItem = null;
        private readonly ILetterSimpleSetFactory<T> _Factory;

        internal SingleSet(ILetterSimpleSetFactory<T> Factory)
        {
            _Factory = Factory;
        }

        internal SingleSet(ILetterSimpleSetFactory<T> Factory, T item)
        {
            _Factory = Factory;
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
            if (_SingleItem == item)
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
                yield return _SingleItem;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return (_SingleItem!=null) ? 1 : 0; }
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
            return _Factory.GetDefault(_SingleItem, item);
        }

        public ILetterSimpleSet<T> Remove(T item, out bool success)
        {
            success = Remove(item);
            return this;
        }
    }
}
