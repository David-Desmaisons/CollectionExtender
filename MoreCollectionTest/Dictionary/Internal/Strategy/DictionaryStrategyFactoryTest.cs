using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var res = DictionaryStrategyFactory.GetStrategy<object, object>(5);
            res.Should().BeOfType<UnorderedDictionaryStrategy<object, object>>();
        }

        [Fact]
        public void GetStrategy_Return_OrderedDictionaryStrategy_WithOderedObject()
        {
            var res = DictionaryStrategyFactory.GetStrategy<string, string>(5);
            res.Should().BeOfType<OrderedDictionaryStrategy<string, string>>();
        }
    }
}
