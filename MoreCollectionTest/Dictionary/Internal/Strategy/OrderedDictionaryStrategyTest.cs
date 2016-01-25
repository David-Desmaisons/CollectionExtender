using MoreCollection.Dictionary.Internal;
using MoreCollection.Dictionary.Internal.Strategy;

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
