using FsCheck;
using MoreCollection.Dictionary;
using System;
using System.Collections.Generic;

namespace MoreCollectionTest.Dictionary.Specification
{
    public abstract class DictionaryComandArgumentOperationSpecification
    {
        private Gen<int> Elements => Gen.Choose(0, 6);
        private Gen<Tuple<int,string>> ElementTuples =>  Gen.zip( Gen.Choose(0, 6), Arb.Generate<string>());

        private Gen<Command<IDictionary<int, string>, IDictionary<int, string>>> Build(Func<int, Command<IDictionary<int, string>, IDictionary<int, string>>> builder)
        {
            return Elements.Select(builder);
        }

        private Gen<Command<IDictionary<int, string>, IDictionary<int, string>>> Build(Func<Tuple<int, string>, Command<IDictionary<int, string>, IDictionary<int, string>>> builder)
        {
            return ElementTuples.Select(builder);
        }

        public Gen<Command<IDictionary<int, string>, IDictionary<int, string>>> Next(IDictionary<int, string> value)
        {
            var count = value.Count;
            return Gen.Frequency(Tuple.Create(Math.Max(1, 5 - count), Build(tuple => new AddDictionary(tuple))),
                                  Tuple.Create(1 + 3 * count, Build(i => new RemoveDictionary(i))),
                                  Tuple.Create(1, Gen.Constant<Command<IDictionary<int, string>, IDictionary<int, string>>>(new ClearDictionary())));
        }

        public static Property FromEmpty()
        {
            return new DictionaryComandArgumentOperationSpecificationEmpty().ToProperty();
        }

        public static Property FromMiddle()
        {
            return new DictionaryComandArgumentOperationSpecificationMiddle().ToProperty();
        }

        public static Property FromDictionary()
        {
            return new DictionaryComandArgumentOperationSpecificationDictionary().ToProperty();
        }

        private class DictionaryComandArgumentOperationSpecificationEmpty : DictionaryComandArgumentOperationSpecification, ICommandGenerator<IDictionary<int, string>, IDictionary<int, string>>
        {
            public IDictionary<int, string> InitialActual => new HybridDictionary<int, string>(1);

            public IDictionary<int, string> InitialModel => new Dictionary<int, string>();
        }

        private class DictionaryComandArgumentOperationSpecificationMiddle : DictionaryComandArgumentOperationSpecification, ICommandGenerator<IDictionary<int, string>, IDictionary<int, string>>
        {
            public IDictionary<int, string> InitialActual => new HybridDictionary<int, string>(3);

            public IDictionary<int, string> InitialModel => new Dictionary<int, string>();
        }

        private class DictionaryComandArgumentOperationSpecificationDictionary : DictionaryComandArgumentOperationSpecification, ICommandGenerator<IDictionary<int, string>, IDictionary<int, string>>
        {
            public IDictionary<int, string> InitialActual => new HybridDictionary<int, string>(20);

            public IDictionary<int, string> InitialModel => new Dictionary<int, string>();
        }
    }
}
