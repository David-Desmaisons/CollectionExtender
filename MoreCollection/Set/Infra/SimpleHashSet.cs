using System.Collections.Generic;

namespace MoreCollection.Set.Infra
{
    internal class SimpleHashSet<T> : HashSet<T>, ILetterSimpleSet<T>
    {
        private readonly ILetterSimpleSetFactory<T> _Factory;
        public SimpleHashSet(ILetterSimpleSetFactory<T> Factory)
        {
            _Factory = Factory;
        }

        public SimpleHashSet(ILetterSimpleSetFactory<T> Factory, IEnumerable<T> collection)
            : base(collection)
        {
            _Factory = Factory;
        }

        public ILetterSimpleSet<T> Add(T item, out bool success)
        {
            success = Add(item);
            return this;
        }

        public ILetterSimpleSet<T> Remove(T item, out bool success)
        {
            success = this.Remove(item);
            return _Factory.OnRemove(this);
        }
    }
}
