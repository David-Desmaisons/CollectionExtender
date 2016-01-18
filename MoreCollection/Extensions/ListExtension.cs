using System;
using System.Collections.Generic;

namespace MoreCollection.Extensions
{
    public static class ListExtension
    {
        public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> enumerable)
        {
            if (list == null)
                throw new ArgumentNullException("enumerable");

            list.ForEach(list.Add);
            return list;
        }
    }
}
