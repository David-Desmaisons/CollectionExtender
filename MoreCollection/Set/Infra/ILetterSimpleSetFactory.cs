using System.Collections.Generic;

namespace MoreCollection.Set.Infra
{
    internal interface ILetterSimpleSetFactory 
    {
        ILetterSimpleSet<T> GetDefault<T>();
        ILetterSimpleSet<T> GetDefault<T>(IEnumerable<T> items);
        ILetterSimpleSet<T> GetDefault<T>(T item);
        ILetterSimpleSet<T> GetDefault<T>(T first, T added);
        ILetterSimpleSet<T> OnAdd<T>(ILetterSimpleSet<T> current);
        ILetterSimpleSet<T> OnRemove<T>(SimpleHashSet<T> current);
    }
}
