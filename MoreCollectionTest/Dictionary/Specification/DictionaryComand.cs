using FsCheck;
using MoreCollectionTest.FsCheckHelper;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollectionTest.Dictionary.Specification
{
    internal abstract class DictionaryComand : CommandInterface<IDictionary<int, string>>
    {
        public override Property Post(IDictionary<int, string> hybrid, IDictionary<int, string> model)
        {
            return model.OrderBy(kvp => kvp.Key).SequenceEqual(hybrid.OrderBy(kvp => kvp.Key))
                        .Label($"Same collection compared from model. Expected:[{(string.Join(",", model))}], actual:[{(string.Join(",", hybrid))}]")
                        .And(hybrid.Count == model.Count).Label($"Count expected:{model.Count} actual {hybrid.Count}");
        }
    }
}
