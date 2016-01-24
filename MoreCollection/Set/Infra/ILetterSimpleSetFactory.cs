using System.Collections.Generic;

namespace MoreCollection.Set.Infra
{
    internal interface ILetterSimpleSetFactory<T> where T : class
    {
        ILetterSimpleSet<T> GetDefault();
        ILetterSimpleSet<T> GetDefault(IEnumerable<T> Items);
        ILetterSimpleSet<T> GetDefault(T Item);
        ILetterSimpleSet<T> GetDefault(T first, T added);
        ILetterSimpleSet<T> OnAdd(ILetterSimpleSet<T> current);
        ILetterSimpleSet<T> OnRemove(SimpleHashSet<T> current);
    }
}
