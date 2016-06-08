using System;
using System.Collections.Generic;

namespace MoreCollection.Composed 
{
    public interface IPriorityQueue<T> : IEnumerable<T> {
        T Dequeue();

        T Peek();

        void Enqueue(T item);

        int Count { get; }

        void CopyTo(Array array, int index);

        IComparer<T> ItemComparer { get; }
    }
}
