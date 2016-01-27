using System.Collections.Generic;
using System.Linq;
using MoreCollection.Set.Infra;
using MoreCollection.Extensions;
using MoreCollection.Infra;

namespace MoreCollection.Set
{
    public class HybridSet<T> : ISet<T> where T: class
    {
        private ILetterSimpleSet<T> _Letter;
        private ILetterSimpleSetFactory<T> _Factory;

        internal HybridSet(int MaxList=10)
        {
            _Factory = GetLetterFactory<T>(MaxList);
            _Letter = _Factory.GetDefault();
        }

        internal HybridSet(T firstitem, int MaxList = 10)
        {
            _Factory = GetLetterFactory<T>(MaxList);
            _Letter = _Factory.GetDefault(firstitem);
        }

        internal HybridSet(IEnumerable<T> items, int MaxList = 10)
        {
            _Factory = GetLetterFactory<T>(MaxList);
            _Letter = _Factory.GetDefault(items);
        }

        private static ILetterSimpleSetFactory<TElement> GetLetterFactory<TElement>(int MaxList) where TElement: class
        {
            return LetterSimpleSetFactoryBuilder<TElement>.GetFactory(MaxList);
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
            _Letter = _Factory.GetDefault(other.Where(_Letter.Contains));
        }

        public void ExceptWith(IEnumerable<T> other) 
        {
            var otherHashSet = new HashSet<T>(other);
            _Letter = _Factory.GetDefault(_Letter.Where(n => !otherHashSet.Contains(n)));
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            var otherHashSet = new HashSet<T>(other);
            var first  = _Letter.Where(n => !otherHashSet.Contains(n));
            var second = otherHashSet.Where(n => !_Letter.Contains(n));
            _Letter = _Factory.GetDefault(first.Concat(second));
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
            _Letter = _Factory.GetDefault();
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

        public int Count
        {
            get { return _Letter.Count; }
        }

        public bool IsReadOnly 
        { 
            get { return false; } 
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
