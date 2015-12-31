using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Extensions
{
    public static class IListExtension
    {
        static public IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> enumerable)
        {
            list.ForEach(t => list.Add(t));
            return list;
        }
    }
}
