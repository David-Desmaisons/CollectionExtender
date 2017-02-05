using FsCheck;
using MoreCollection.Set;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollectionTest.Set.Specification
{
    public class OperationSpecification : ICommandGenerator<ISet<int>, ISet<int>>
    {
        public ISet<int> InitialActual => new HybridSet<int>(3);

        public ISet<int> InitialModel => new HashSet<int>();

        private Gen<int> Elements => Gen.Choose(0, 10);

        private Gen<Command<ISet<int>, ISet<int>>> Build(Func<int, Command<ISet<int>, ISet<int>>> builder)
        {
            return Elements.Select(builder);
        }

        public Gen<Command<ISet<int>, ISet<int>>> Next(ISet<int> value)
        {
            var count = value.Count;
            return Gen.Frequency( Tuple.Create(Math.Max( 1, 5 - count), Build(i => new Add(i))),
                                  Tuple.Create(1 + 3 * count, Build(i => new Remove(i))),
                                  Tuple.Create(1, Gen.Constant<Command<ISet<int>, ISet<int>>>(new Clear())));
        }
    }
}
