using MoreCollection.Dictionary.Internal;
using MoreCollectionTest.TestInfra;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class ListDictionaryTest : DictionaryTest
    {
        public ListDictionaryTest()
        {
            DictionaryStrategyFactory<string>.Strategy = DictionaryStrategyFactory<string>.GetStrategy();
            _dictionary = new ListDictionary<string, string>();
        }

        [Fact]
        public void Constructor_WithDictionary_CreateDictiionaryWithSameKey()
        {
            var dict = new Dictionary<string, string>(){
                {"a", "A"}, {"b", "B"}, {"c", "C"}
            };
            var target = new ListDictionary<string, string>(dict);
            target.AsEnumerable().Should().BeEquivalentTo(dict);
        }
    }
}
