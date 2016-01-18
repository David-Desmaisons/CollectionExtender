using MoreCollection.Set.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;

namespace MoreCollection.Set
{
    public class PolyMorphSet<T> : ISet<T> where T: class
    {
        private ILetterSimpleSet<T> _Letter;

        internal PolyMorphSet()
        {
            _Letter = LetterSimpleSetFactory<T>.Factory.GetDefault();
        }

        internal PolyMorphSet(T firstitem)
        {
            _Letter = LetterSimpleSetFactory<T>.Factory.GetDefault(firstitem);
        }

        internal PolyMorphSet(IEnumerable<T> firstitem)
        {
            _Letter = LetterSimpleSetFactory<T>.Factory.GetDefault(firstitem);
        }

        public bool Add(T item)
        {
            bool res;
            _Letter = _Letter.Add(item, out res);
            return res;
        }

        public void UnionWith(IEnumerable<T> other) {
            other.ForEach(t => Add(t));
        }

        public void IntersectWith(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other) {
            var otherHashSet = new HashSet<T>(other);
            return _Letter.All(otherHashSet.Contains);
        }

        public bool IsSupersetOf(IEnumerable<T> other) {
            return other.All(_Letter.Contains);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other) {
            return other.Any(_Letter.Contains);
        }

        public bool SetEquals(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        public void Clear() {
            _Letter = LetterSimpleSetFactory<T>.Factory.GetDefault();
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            bool res;
            _Letter = _Letter.Remove(item, out res);
            return res;
        }

        void ICollection<T>.Add(T item) {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            return _Letter.Contains(item);
        }

        public int Count
        {
            get { return _Letter.Count; }
        }

        public bool IsReadOnly { get; }

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
