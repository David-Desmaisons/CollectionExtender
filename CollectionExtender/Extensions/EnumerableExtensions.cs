using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MoreCollection.Extensions
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            foreach (var o in enumerable)
            {
                action(o);
            }

            return enumerable;
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T,int> action)
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
        public static void ForEachCartesian<TSource1, TSource2>(this IEnumerable<TSource1> first,
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
            return enumerable.Indexes(t => Object.Equals(t, value)); 
        }

        public static int Index<T>(this IEnumerable<T> enumerable, Func<T, bool> Selector)
        {
            return enumerable.Indexes(Selector).FirstOrDefault(-1);
        }

        public static int Index<T>(this IEnumerable<T> enumerable, T value)
        {
            return enumerable.Index((t) => object.Equals(t, value));
        }

        private static IEnumerable<TResult> ZipInternal<TResult, TSource1, TSource2, TSource3>(IEnumerable<TSource1> enumerable,
                               IEnumerable<TSource2> enumerable2, IEnumerable<TSource3> enumerable3,
                               Func<TSource1, TSource2, TSource3, TResult> Agregate)
        {
            using (var e1 = enumerable.GetEnumerator())
            {
                using (var e2 = enumerable2.GetEnumerator())
                {
                    using (var e3 = enumerable3.GetEnumerator())
                    {
                        while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                            yield return Agregate(e1.Current, e2.Current, e3.Current);
                    }
                }
            }
        }

        public static IEnumerable<TResult> Zip<TResult, TSource1, TSource2, TSource3>(this IEnumerable<TSource1> enumerable,
                               IEnumerable<TSource2> enumerable2, IEnumerable<TSource3> enumerable3,
                               Func<TSource1, TSource2, TSource3, TResult> Agregate)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            if (enumerable2 == null)
                throw new ArgumentNullException("enumerable2");

            if (enumerable3 == null)
                throw new ArgumentNullException("enumerable3");

            return ZipInternal(enumerable, enumerable2, enumerable3, Agregate);
        }
    }
}
