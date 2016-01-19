using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Infra
{
    internal static class EnumerableHelper
    {
        internal static void CopyTo<T>(IEnumerable<T> enumerable, T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException();

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();

            int index = arrayIndex;

            foreach (var item in enumerable)
                array[index++] = item;
        }
    }
}
