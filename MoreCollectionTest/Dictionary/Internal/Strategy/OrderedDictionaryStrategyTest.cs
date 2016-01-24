using MoreCollection.Dictionary.Internal;
using MoreCollection.Dictionary.Internal.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollectionTest.Dictionary.Internal.Strategy
{
    public class OrderedDictionaryStrategyTest : DictionaryStrategyTest<MutableSortedDictionary<string, string>>
    {
        public OrderedDictionaryStrategyTest()
        {
            DictionaryStrategy = new OrderedDictionaryStrategy<string, string>(_Transition);
        }
    }
}
