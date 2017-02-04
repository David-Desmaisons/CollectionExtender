using System.Collections.Generic;

namespace MoreCollection.Set.Infra
{
    public interface ILetterSimpleSet<T> : IEnumerable<T>
    {
        ILetterSimpleSet<T> Add(T item, out bool success);

        ILetterSimpleSet<T> Remove(T item, out bool success);

        bool Contains(T item);

        int Count { get; }
    }
}
