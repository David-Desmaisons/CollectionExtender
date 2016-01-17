using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Set.Infra
{
    public class SingleSet<T> : ILetterSimpleSet<T> where T : class
    {
        private T _SingleItem = null;

        internal SingleSet()
        {
        }

        internal SingleSet(T item)
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
                
            return (Object.Equals(item, _SingleItem)) ? false : new Nullable<bool>();
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
            Nullable<bool> res = Add(item);
            if (res != null)
            {
                success = res.Value;
                return this;
            }

            return new ListSet<T>(_SingleItem).Add(item, out success);
        }

        public ILetterSimpleSet<T> Remove(T item, out bool success)
        {
            success = Remove(item);
            return this;
        }
    }
}
