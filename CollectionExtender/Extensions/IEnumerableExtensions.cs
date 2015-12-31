using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Extensions
{
    public static class IEnumerableExtensions
    {

        [DebuggerStepThrough]
        static public IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                return null;

            foreach (T o in enumerable)
            {
                action(o);
            }

            return enumerable;
        }
    }
}
