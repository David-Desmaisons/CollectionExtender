using System.Collections.Generic;

namespace MoreCollection.Extensions 
{
    public static class IComparerExtender 
    {
        private class RevertComparer<T> : IComparer<T> 
        {
            private IComparer<T> _Comparer;
            internal RevertComparer(IComparer<T> iComparer) 
            {
                _Comparer = iComparer;
            }

            public int Compare(T x, T y) 
            {
                return _Comparer.Compare(y, x);
            }
        }

        public static IComparer<T> Revert<T>(this IComparer<T> @this) 
        {
            return new RevertComparer<T>(@this);
        }
    }
}
