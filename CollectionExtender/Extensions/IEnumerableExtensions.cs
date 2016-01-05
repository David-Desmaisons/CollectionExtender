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
                throw new ArgumentNullException("enumerable");

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
                throw new ArgumentNullException("enumerable");

            foreach (T o in enumerable)
            {
                action(o);
                if (iCancellationToken.IsCancellationRequested)
                    return false;
            }

            return true;
        }

        [DebuggerStepThrough]
        static public IEnumerable<TResult> Caretesian<TResult, TSource1, TSource2>(this IEnumerable<TSource1> first,
                                IEnumerable<TSource2> second, Func< TSource1, TSource2, TResult> Agregator )
        {
            if (second == null)
                throw new ArgumentNullException("second");

            return first.SelectMany(_ => second, (ts1, ts2) => Agregator(ts1, ts2));
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, Func<T, bool> predicate)
        {
            return enumerable.Where(predicate).DefaultIfEmpty(defaultValue).First(); 
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue)
        {
            return enumerable.DefaultIfEmpty(defaultValue).First();
        }

        static private IEnumerable<Tuple<int, T>> AsIndexed<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select((t, i) => new Tuple<int, T>(i, t));
        }

        static public IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, Func<T, bool> Selector)
        {
            return enumerable.AsIndexed().Where(t => Selector(t.Item2)).Select(t => t.Item1);
        }

        static public IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, T value)
        {
            return IEnumerableExtensions.Indexes(enumerable, t => object.Equals(t, value)); 
        }

        static public int Index<T>(this IEnumerable<T> enumerable, Func<T, bool> Selector)
        {
            return IEnumerableExtensions.Indexes(enumerable, Selector).FirstOrDefault(-1);
        }

        static public int Index<T>(this IEnumerable<T> enumerable, T value)
        {
            return enumerable.Index((t) => object.Equals(t, value));
        }
    }
}
