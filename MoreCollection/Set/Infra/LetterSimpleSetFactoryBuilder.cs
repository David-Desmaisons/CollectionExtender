using System;
using System.Collections.Concurrent;

namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactoryBuilder<T>
    {
        private static readonly ConcurrentDictionary<int, LetterSimpleSetFactory<T>> _Factory = new ConcurrentDictionary<int, LetterSimpleSetFactory<T>>();
        internal static Func<int, ILetterSimpleSetFactory<T>> GetFactory { get; set; } =  (maxList) => _Factory.GetOrAdd(maxList, _ => new LetterSimpleSetFactory<T>(maxList));
    }
}
