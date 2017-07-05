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
            var res = DictionaryStrategyFactory<object>.GetStrategy();
            res.Should().BeOfType<UnorderedDictionaryStrategy>();
        }

        [Fact]
        public void GetStrategy_Return_OrderedDictionaryStrategy_WithOderedObject()
        {
            var res = DictionaryStrategyFactory<string>.GetStrategy();
            res.Should().BeOfType<OrderedDictionaryStrategy>();
        }
    }
}
