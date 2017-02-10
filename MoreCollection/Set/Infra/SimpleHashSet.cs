using System.Collections.Generic;

namespace MoreCollection.Set.Infra
{
    internal class SimpleHashSet<T> : HashSet<T>, ILetterSimpleSet<T>
    {
        private readonly ILetterSimpleSetFactory _Factory;
        public SimpleHashSet(ILetterSimpleSetFactory factory)
        {
            _Factory = factory;
        }

        public SimpleHashSet(ILetterSimpleSetFactory factory, IEnumerable<T> collection): base(collection)
        {
            _Factory = factory;
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
