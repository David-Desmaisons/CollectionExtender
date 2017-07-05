using FsCheck;
using MoreCollection.Set;
using MoreCollectionTest.FsCheckHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollectionTest.Set.Specification
{
    public abstract class SetOperationSpecification
    {
        private Gen<int> Elements => Gen.Choose(0, 6);

        private Gen<Command<ISet<int>, ISet<int>>> Build(Func<int, Command<ISet<int>, ISet<int>>> builder)
        {
            return Elements.Select(builder);
        }

        public Gen<Command<ISet<int>, ISet<int>>> Next(ISet<int> value)
        {
            var count = value.Count;
            return Gen.Frequency( Tuple.Create(Math.Max( 1, 5 - count), Build(i => new AddSet(i))),
                                  Tuple.Create(1 + 3 * count, Build(i => new RemoveSet(i))),
                                  Tuple.Create(1, Gen.Constant<Command<ISet<int>, ISet<int>>>(new ClearSet())));
        }

        public static Property FromEmpty()
        {
            return new SetOperationSpecificationFromEmpty().ToProperty();
        }

        public static Property FromSingle()
        {
            return new SetOperationSpecificationFromSingle().ToProperty();
        }

        public static Property FromList()
        {
            return new SetOperationSpecificationFromCollection(2).ToProperty();
        }

        public static Property FromHash()
        {
            return new SetOperationSpecificationFromCollection(6).ToProperty();
        }

        private class SetOperationSpecificationFromEmpty : SetOperationSpecification, ICommandGenerator<ISet<int>, ISet<int>>
        {
            public ISet<int> InitialActual => new HybridSet<int>();
            public ISet<int> InitialModel => new HashSet<int>();
        }

        private class SetOperationSpecificationFromSingle : SetOperationSpecification, ICommandGenerator<ISet<int>, ISet<int>>
        {
            public ISet<int> InitialActual => new HybridSet<int>(Value);
            public ISet<int> InitialModel => new HashSet<int> { Value };
            private int Value { get; } = Gen.Choose(0, 6).Generate();
        }

        private class SetOperationSpecificationFromCollection : SetOperationSpecification, ICommandGenerator<ISet<int>, ISet<int>>
        {
            public ISet<int> InitialActual => new HybridSet<int>(Values);
            public ISet<int> InitialModel => new HashSet<int>(Values);
            private List<int> Values { get; }

            public SetOperationSpecificationFromCollection(int count)
            {
                Values = Gen.Choose(0, 6).ListOf(count).Where(l => l.Distinct().Count() == l.Count).Generate().ToList();
            }
        }
    }
}
