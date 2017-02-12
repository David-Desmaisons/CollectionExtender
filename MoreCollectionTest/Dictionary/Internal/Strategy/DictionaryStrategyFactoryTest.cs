using FluentAssertions;
using Xunit;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal.Strategy
{
    public class DictionaryStrategyFactoryTest
    {
        [Fact]
        public void GetStrategy_Return_UnorderedDictionaryStrategy_WithUnorderedObject()
        {
            var res = DictionaryStrategyFactory.GetStrategy<object>();
            res.Should().BeOfType<UnorderedDictionaryStrategy<object>>();
        }

        [Fact]
        public void GetStrategy_Return_OrderedDictionaryStrategy_WithOderedObject()
        {
            var res = DictionaryStrategyFactory.GetStrategy<string>();
            res.Should().BeOfType<OrderedDictionaryStrategy<string>>();
        }
    }
}
