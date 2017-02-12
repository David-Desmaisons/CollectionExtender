using MoreCollection.Dictionary.Internal;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal.Strategy
{
    public class OrderedDictionaryStrategyTest : DictionaryStrategyTest<MutableSortedListDictionary<string, string>>
    {
        public OrderedDictionaryStrategyTest()
        {
            DictionaryStrategy = new OrderedDictionaryStrategy(_Transition);
        }
    }
}
