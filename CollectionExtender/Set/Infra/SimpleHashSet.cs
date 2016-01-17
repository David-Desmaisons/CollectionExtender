using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Set.Infra
{
    internal class SimpleHashSet<T> : HashSet<T>, ILetterSimpleSet<T> where T : class
    {
        public SimpleHashSet()
        {
        }

        public SimpleHashSet(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ILetterSimpleSet<T> Add(T item, out bool success)
        {
            success = this.Add(item);
            return this;
        }

        public ILetterSimpleSet<T> Remove(T item, out bool success)
        {
            success = this.Remove(item);
            if (Count == LetterSimpleSetFactory<T>.MaxList-1)
            {
                return new ListSet<T>(this);
            }
            return this;
        }
    }
}
