using MoreCollection.Composed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreCollection.Extensions
{
    public static class EnumerableAlgoExtender
    {

        public static ICollection<T> SortFirst<T>(this IEnumerable<T> @this, int iFirst, IComparer<T> comparer = null, bool sameElements = false)
        {
            if (@this == null)
                throw new ArgumentNullException();

            if (iFirst <= 0)
                throw new ArgumentException("iFirst");

            var pq = new PriorityQueue<T>(comparer, iFirst + 1);

            foreach (T el in @this.Take(iFirst))
            {
                pq.Enqueue(el);
            }

            var notOK = sameElements ? new List<T>() : null;

            foreach (T el in @this.Skip(iFirst))
            {
                if (pq.ItemComparer.Compare(el, pq.Peek()) < 0)
                {
                    pq.Enqueue(el);
                    var no = pq.Dequeue();
                    if (notOK != null)
                        notOK.Add(no);
                }
                else if (notOK != null)
                    notOK.Add(el);

            }

            var res = new LinkedList<T>();
            while (pq.Count > 0)
            {
                res.AddFirst(pq.Dequeue());
            }

            if (notOK != null)
                notOK.ForEach(n => res.AddLast(n));

            return res;
        }

        public static ICollection<T> SortLast<T>(this IEnumerable<T> @this, int first, IComparer<T> comparer = null)
        {
            IComparer<T> IntComparer = comparer ?? Comparer<T>.Default;
            return @this.SortFirst(first, IntComparer.Revert());
        }

        private static void Merge<T>(this PriorityQueue<T> first, PriorityQueue<T> second)
        {
            bool needtoinsert = false;
            while (second.Count > 0)
            {
                T el = second.Dequeue();
                if ((needtoinsert) || (first.ItemComparer.Compare(el, first.Peek()) < 0))
                {
                    first.Enqueue(el);
                    first.Dequeue();
                    needtoinsert = true;
                }
            }
        }

        public static ICollection<T> SortFirstParallel<T>(this IEnumerable<T> @this, int first, IComparer<T> comparer = null)
        {
            if (@this == null)
                throw new ArgumentNullException();

            if (first <= 0)
                throw new ArgumentException("iFirst");

            if (10 * first > @this.Count())
            {
                return @this.SortFirst(first, comparer);
            }

            var res = new LinkedList<T>();
            PriorityQueue<T> refpq = null;

            Parallel.ForEach(@this,
                   () => new PriorityQueue<T>(comparer, first + 1),
                   (el, lc, localqueue) =>
                   {
                       if (localqueue.Count < first)
                       {
                           localqueue.Enqueue(el);
                       }
                       else if (localqueue.ItemComparer.Compare(el, localqueue.Peek()) < 0)
                       {
                           localqueue.Enqueue(el);
                           localqueue.Dequeue();
                       }
                       return localqueue;
                   },
                   (llist) =>
                   {
                       lock (res)
                       {
                           if (refpq == null)
                               refpq = llist;
                           else
                               refpq.Merge(llist);
                       }
                   });


            while (refpq.Count > 0)
            {
                res.AddFirst(refpq.Dequeue());
            }

            return res;
        }

        public static T MinBy<T>(this IEnumerable<T> @this, IComparer<T> comparer = null) where T : class
        {
            if (@this == null)
                throw new ArgumentNullException();

            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            using (IEnumerator<T> sourceIterator = @this.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    return null;
                }

                T min = sourceIterator.Current;

                while (sourceIterator.MoveNext())
                {
                    T candidate = sourceIterator.Current;
                    if (comparer.Compare(candidate, min) < 0)
                    {
                        min = candidate;
                    }
                }
                return min;
            }
        }

        public static T MaxBy<T>(this IEnumerable<T> @this, IComparer<T> comparer = null) where T : class
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            return @this.MinBy(comparer.Revert());
        }
    }
}
