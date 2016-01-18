using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Set
{
    public interface ISet<T>: IEnumerable<T> where T : class
    {
        bool Add(T item);

        bool Remove(T item);

        bool Contains(T item);

        int Count { get; }
    }
}
