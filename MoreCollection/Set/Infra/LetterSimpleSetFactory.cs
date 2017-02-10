using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactory : ILetterSimpleSetFactory
    {
        private readonly int _MaxList;

        internal LetterSimpleSetFactory(int maxList)
        {
            _MaxList = maxList;
        }

        public ILetterSimpleSet<T> GetDefault<T>()
        {
            return new SingleSet<T>(this);
        }

        public ILetterSimpleSet<T> GetDefault<T>(T Item)
        {
            return new SingleSet<T>(this,Item);
        }

        public ILetterSimpleSet<T> GetDefault<T>(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var filteredItems = new HashSet<T>(items);

            var count = filteredItems.Count;
            if (count >= _MaxList)
                return new SimpleHashSet<T>(this, filteredItems);

            if (count > 1)
                return new ListSet<T>(this, filteredItems, _MaxList);

            return count == 1 ? GetDefault(filteredItems.First()) : GetDefault<T>();
        }

       public ILetterSimpleSet<T> OnAdd<T>(ILetterSimpleSet<T> current)
       {
           return (current.Count == _MaxList) ? new SimpleHashSet<T>(this, current) : current;
       }

       public ILetterSimpleSet<T> OnRemove<T>(SimpleHashSet<T> current)
       {
           ILetterSimpleSet<T> listSet = current;
           return (current.Count == _MaxList - 1) ? new ListSet<T>(this, current, _MaxList) : listSet;
       }

       public ILetterSimpleSet<T> GetDefault<T>(T item, T added)
       {
           bool success;
           return new ListSet<T>(this, item, _MaxList).Add(added, out success);
       }
    }
}
