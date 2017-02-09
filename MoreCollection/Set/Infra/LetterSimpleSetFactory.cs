using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactory<T> : ILetterSimpleSetFactory<T>
    {
        private readonly int _MaxList;

        internal LetterSimpleSetFactory(int maxList)
        {
            _MaxList = maxList;
        }

        public ILetterSimpleSet<T> GetDefault()
        {
            return new SingleSet<T>(this);
        }

        public ILetterSimpleSet<T> GetDefault(T Item)
        {
            return new SingleSet<T>(this,Item);
        }

        public ILetterSimpleSet<T> GetDefault(IEnumerable<T> Items)
        {
            if (Items == null)
                throw new ArgumentNullException(nameof(Items));

            var FiItems = new HashSet<T>(Items);

            var count = FiItems.Count;
            if (count >= _MaxList)
                return new SimpleHashSet<T>(this, FiItems);

            if (count > 1)
                return new ListSet<T>(this, FiItems, _MaxList);

            return count == 1 ? GetDefault(FiItems.First()) : GetDefault();
        }

       public ILetterSimpleSet<T> OnAdd(ILetterSimpleSet<T> current)
       {
           return (current.Count == _MaxList) ? new SimpleHashSet<T>(this, current) : current;
       }

       public ILetterSimpleSet<T> OnRemove(SimpleHashSet<T> current)
       {
           ILetterSimpleSet<T> ListSet = current;
           return (current.Count == _MaxList - 1) ? new ListSet<T>(this, current, _MaxList) : ListSet;
       }

       public ILetterSimpleSet<T> GetDefault(T item, T added)
       {
           bool success;
           return new ListSet<T>(this, item, _MaxList).Add(added, out success);
       }
    }
}
