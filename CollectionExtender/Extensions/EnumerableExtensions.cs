using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionExtender.Extensions
{
    public static class EnumerableExtensions
    {

        [DebuggerStepThrough]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
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
        static public IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T,int> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            int i = 0;
            foreach (T o in enumerable)
            {
                action(o, i++);
            }

            return enumerable;
        }

        [DebuggerStepThrough]
        public static bool ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, CancellationToken iCancellationToken)
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
        public static IEnumerable<TResult> Cartesian<TResult, TSource1, TSource2>(this IEnumerable<TSource1> first,
                                IEnumerable<TSource2> second, Func< TSource1, TSource2, TResult> Agregator )
        {
            if (second == null)
                throw new ArgumentNullException("second");

            return first.SelectMany(_ => second, (ts1, ts2) => Agregator(ts1, ts2));
        }

        [DebuggerStepThrough]
        public static void ForCartesian<TSource1, TSource2>(this IEnumerable<TSource1> first,
                                IEnumerable<TSource2> second, Action<TSource1, TSource2> Do)
        {
            if (second == null)
                throw new ArgumentNullException("second");

            first.SelectMany(_ => second, (ts1, ts2) => new { TSource1 = ts1, TSource2 = ts2 })
                .ForEach(t => Do(t.TSource1, t.TSource2));
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, Func<T, bool> predicate)
        {
            return enumerable.Where(predicate).DefaultIfEmpty(defaultValue).First(); 
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue)
        {
            return enumerable.DefaultIfEmpty(defaultValue).First();
        }

        private static IEnumerable<Tuple<int, T>> AsIndexed<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select((t, i) => new Tuple<int, T>(i, t));
        }

        public static IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, Func<T, bool> Selector)
        {
            return enumerable.AsIndexed().Where(t => Selector(t.Item2)).Select(t => t.Item1);
        }

        public static IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, T value)
        {
            return EnumerableExtensions.Indexes(enumerable, t => object.Equals(t, value)); 
        }

        public static int Index<T>(this IEnumerable<T> enumerable, Func<T, bool> Selector)
        {
            return EnumerableExtensions.Indexes(enumerable, Selector).FirstOrDefault(-1);
        }

        public static int Index<T>(this IEnumerable<T> enumerable, T value)
        {
            return enumerable.Index((t) => object.Equals(t, value));
        }
    }
}
