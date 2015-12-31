using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionExtender.Extensions
{
    public static class IEnumerableExtensions
    {

        [DebuggerStepThrough]
        static public IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                throw new NullReferenceException("enumerable");

            foreach (T o in enumerable)
            {
                action(o);
            }

            return enumerable;
        }

        [DebuggerStepThrough]
        static public bool ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, CancellationToken iCancellationToken)
        {
            if (enumerable == null)
                throw new NullReferenceException("enumerable");

            foreach (T o in enumerable)
            {
                action(o);
                if (iCancellationToken.IsCancellationRequested)
                    return false;
            }

            return true;
        }
    }
}
