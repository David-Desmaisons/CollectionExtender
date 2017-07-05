using System.Collections.Generic;
using System.Linq;
using MoreCollection.Set.Infra;
using MoreCollection.Extensions;
using MoreCollection.Infra;
using System.Diagnostics;

namespace MoreCollection.Set
{
    [DebuggerDisplay("Count = {Count}")]
    public class HybridSet<T> : ISet<T>
    {
        private ILetterSimpleSet<T> _Letter;

        public int Count => _Letter.Count;
        public bool IsReadOnly => false;
        private static ILetterSimpleSetFactory Factory => LetterSimpleSetFactoryBuilder.Factory;

        public HybridSet()
        {
            _Letter = Factory.GetDefault<T>();
        }

        public HybridSet(T firstitem)
        {
            _Letter = Factory.GetDefault(firstitem);
        }

        public HybridSet(IEnumerable<T> items)
        {
            _Letter = Factory.GetDefault(items);
        }

        public bool Add(T item)
        {
            bool res;
            _Letter = _Letter.Add(item, out res);
            return res;
        }

        public void UnionWith(IEnumerable<T> other) 
        {
            other.ForEach(t => Add(t));
        }

        public void IntersectWith(IEnumerable<T> other) 
        {
            _Letter = LetterSimpleSetFactoryBuilder.Factory.GetDefault(other.Where(_Letter.Contains));
        }

        public void ExceptWith(IEnumerable<T> other) 
        {
            var otherHashSet = new HashSet<T>(other);
            _Letter = LetterSimpleSetFactoryBuilder.Factory.GetDefault(_Letter.Where(n => !otherHashSet.Contains(n)));
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            var otherHashSet = new HashSet<T>(other);
            var first  = _Letter.Where(n => !otherHashSet.Contains(n));
            var second = otherHashSet.Where(n => !_Letter.Contains(n));
            _Letter = LetterSimpleSetFactoryBuilder.Factory.GetDefault(first.Concat(second));
        }

        public bool IsSubsetOf(IEnumerable<T> other) 
        {
            var otherHashSet = new HashSet<T>(other);
            return _Letter.All(otherHashSet.Contains);
        }

        public bool IsSupersetOf(IEnumerable<T> other) 
        {
            return other.All(_Letter.Contains);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other) 
        {
            var otherHashed = new HashSet<T>(other);
            return ((otherHashed.Count != Count) && otherHashed.All(_Letter.Contains));
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) 
        {
            var otherHashed = new HashSet<T>(other);
            return ((otherHashed.Count != Count) && _Letter.All(otherHashed.Contains));
        }

        public bool Overlaps(IEnumerable<T> other) 
        {
            return other.Any(_Letter.Contains);
        }

        public bool SetEquals(IEnumerable<T> other) 
        {
            var otherHashed = new HashSet<T>(other);
            return ((otherHashed.Count == Count) && otherHashed.All(_Letter.Contains));
        }

        public void Clear() 
        {
            _Letter = Factory.GetDefault<T>();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            EnumerableHelper.CopyTo(this, array, arrayIndex);
        }

        public bool Remove(T item)
        {
            bool res;
            _Letter = _Letter.Remove(item, out res);
            return res;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public bool Contains(T item)
        {
            return _Letter.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _Letter.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
