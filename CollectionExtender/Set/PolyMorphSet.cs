using CollectionExtender.Set.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Set
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

        public bool Remove(T item)
        {
            bool res;
            _Letter = _Letter.Remove(item, out res);
            return res;
        }

        public bool Contains(T item)
        {
            return _Letter.Contains(item);
        }

        public int Count
        {
            get { return _Letter.Count; }
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
