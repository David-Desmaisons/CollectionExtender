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
