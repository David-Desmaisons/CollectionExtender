using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoreCollection.Extensions
{
    public static class ListExtension
    {
        public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> enumerable)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            list.ForEach(list.Add);
            return list;
        }

        public static IList<T> Move<T>(this IList<T> list, int oldIndex, int newIndex)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            var observable = list as ObservableCollection<T>;
            if (observable != null)
            {
                observable.Move(oldIndex,newIndex);
                return list;
            }

            var count = list.Count;
            if ((oldIndex < 0) || (oldIndex > count - 1))
                throw new ArgumentOutOfRangeException("oldIndex");

            if ((newIndex < 0) || (newIndex > count - 1))
                throw new ArgumentOutOfRangeException("newIndex");

            var item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
            return list;
        }
    }
}
