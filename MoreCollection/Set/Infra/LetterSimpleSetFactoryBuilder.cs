using System;
using System.Collections.Concurrent;

namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactoryBuilder
    {
        private static readonly ConcurrentDictionary<int, LetterSimpleSetFactory> _Factory = new ConcurrentDictionary<int, LetterSimpleSetFactory>();
        internal static Func<int, ILetterSimpleSetFactory> GetFactory { get; set; } =  (maxList) => _Factory.GetOrAdd(maxList, _ => new LetterSimpleSetFactory(maxList));
    }
}
