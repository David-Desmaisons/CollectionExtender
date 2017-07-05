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
                throw new ArgumentNullException(nameof(enumerable));

            foreach (var o in enumerable) {
                action(o);
            }

            return enumerable;
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action) 
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            int i = 0;
            foreach (T o in enumerable) {
                action(o, i++);
            }

            return enumerable;
        }

        [DebuggerStepThrough]
        public static bool ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, CancellationToken cancellationToken) 
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            foreach (T o in enumerable) 
            {
                action(o);
                if (cancellationToken.IsCancellationRequested)
                    return false;
            }

            return true;
        }

        public static void ZipForEach<TSource1, TSource2>(this IEnumerable<TSource1> enumerable,
                       IEnumerable<TSource2> enumerable2,  Action<TSource1, TSource2> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (enumerable2 == null)
                throw new ArgumentNullException(nameof(enumerable2));

            using (var e1 = enumerable.GetEnumerator())
            {
                using (var e2 = enumerable2.GetEnumerator())
                {
                    while (e1.MoveNext() && e2.MoveNext())
                         action(e1.Current, e2.Current);
                }
            }
        }

        [DebuggerStepThrough]
        public static IEnumerable<TResult> Cartesian<TResult, TSource1, TSource2>(this IEnumerable<TSource1> first,
                                IEnumerable<TSource2> second, Func<TSource1, TSource2, TResult> agregator) 
        {
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            return first.SelectMany(_ => second, (ts1, ts2) => agregator(ts1, ts2));
        }

        [DebuggerStepThrough]
        public static void ForEachCartesian<TSource1, TSource2>(this IEnumerable<TSource1> first,
                                IEnumerable<TSource2> second, Action<TSource1, TSource2> Do) {
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            first.SelectMany(_ => second, (ts1, ts2) => new { TSource1 = ts1, TSource2 = ts2 })
                .ForEach(t => Do(t.TSource1, t.TSource2));
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, Func<T, bool> predicate) 
        {
            return enumerable.Where(predicate).DefaultIfEmpty(defaultValue).First();
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue) {
            return enumerable.DefaultIfEmpty(defaultValue).First();
        }

        private static IEnumerable<Tuple<int, T>> AsIndexed<T>(this IEnumerable<T> enumerable) {
            return enumerable.Select((t, i) => new Tuple<int, T>(i, t));
        }

        public static IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, Func<T, bool> selector)
        {
            return enumerable.AsIndexed().Where(t => selector(t.Item2)).Select(t => t.Item1);
        }

        public static IEnumerable<int> Indexes<T>(this IEnumerable<T> enumerable, T value) 
        {
            return enumerable.Indexes(t => Object.Equals(t, value));
        }

        public static int Index<T>(this IEnumerable<T> enumerable, Func<T, bool> selector) 
        {
            return enumerable.Indexes(selector).FirstOrDefault(-1);
        }

        public static int Index<T>(this IEnumerable<T> enumerable, T value) {
            return enumerable.Index((t) => object.Equals(t, value));
        }

        public static TSource GetMinElement<TSource, TValue>(this IEnumerable<TSource> enumerable, Func<TSource, TValue> getValue) 
            where TValue : IComparable<TValue> where TSource : class 
        {
            return enumerable.Aggregate((curMin, x) => (curMin == null || getValue(x).CompareTo(getValue(curMin)) < 0) ? x : curMin);
        }

        public static TSource GetMaxElement<TSource, TValue>(this IEnumerable<TSource> enumerable, Func<TSource, TValue> getValue)
           where TValue : IComparable<TValue> where TSource : class 
        {
            return enumerable.Aggregate((curMin, x) => (curMin == null || getValue(x).CompareTo(getValue(curMin)) > 0) ? x : curMin);
        }

        private static IEnumerable<TResult> ZipInternal<TResult, TSource1, TSource2, TSource3>(IEnumerable<TSource1> enumerable,
                               IEnumerable<TSource2> enumerable2, IEnumerable<TSource3> enumerable3,
                               Func<TSource1, TSource2, TSource3, TResult> Agregate) {
            using (var e1 = enumerable.GetEnumerator()) {
                using (var e2 = enumerable2.GetEnumerator()) {
                    using (var e3 = enumerable3.GetEnumerator()) {
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
                throw new ArgumentNullException(nameof(enumerable));

            if (enumerable2 == null)
                throw new ArgumentNullException(nameof(enumerable2));

            if (enumerable3 == null)
                throw new ArgumentNullException(nameof(enumerable3));

            return ZipInternal(enumerable, enumerable2, enumerable3, Agregate);
        }


        public static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
    }
}
