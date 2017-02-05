using System;

namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactoryBuilder<T>
    {
        internal static Func<int, ILetterSimpleSetFactory<T>> GetFactory { get; set; } =  (maxList) => new LetterSimpleSetFactory<T>(maxList);
    }
}
