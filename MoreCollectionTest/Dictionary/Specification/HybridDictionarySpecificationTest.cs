using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using MoreCollection.Dictionary;
using MoreCollection.Extensions;

namespace MoreCollectionTest.Dictionary.Specification 
{
    public class HybridDictionarySpecificationTest 
    {

        [Property(MaxTest = 2000)]
        public Property HybridDictionary_BuildFromEmptyBehaveAsDictionary()
        {
            return DictionaryComandArgumentOperationSpecification.FromEmpty();
        }

        [Property(MaxTest = 2000)]
        public Property HybridDictionary_BuildFromMiddleBehaveAsDictionary()
        {
            return DictionaryComandArgumentOperationSpecification.FromMiddle();
        }

        [Property(MaxTest = 2000)]
        public Property HybridDictionary_BuildFromDictionaryBehaveAsDictionary()
        {
            return DictionaryComandArgumentOperationSpecification.FromDictionary();
        }

        [Property(MaxTest = 1000)]
        public Property Constructor_CreateAValidDictionary() {
            var threshold = 10;

            return Prop.ForAll<Tuple<int,string>[]>((keyValues) => {
                var model = new Dictionary<int,string>();
                keyValues.ForEach(kv => model[kv.Item1] = kv.Item2);

                var hybrid = new HybridDictionary<int, string>(threshold);
                keyValues.ForEach(kv => hybrid[kv.Item1] = kv.Item2);

                return model.OrderBy(kvp => kvp.Key).SequenceEqual(hybrid.OrderBy(kvp => kvp.Key))
                            .Classify(hybrid.Count<=1, "Single")
                            .Classify(hybrid.Count > 1 && hybrid.Count<= threshold, "List")
                            .Classify(hybrid.Count > threshold, "Dictionary")
                            .Classify(model.Count!= keyValues.Length, "None trivial")
                            .Classify(model.Count == keyValues.Length, "trivial");
            });
        }
    }
}
