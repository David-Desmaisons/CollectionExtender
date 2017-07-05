using MoreCollection.Dictionary.Internal;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal.Strategy
{
    public class UnorderedDictionaryStrategyTest : DictionaryStrategyTest<MutableListDictionary<string, string>>
    {
        public UnorderedDictionaryStrategyTest()
        {
            DictionaryStrategy = new UnorderedDictionaryStrategy(_Transition);
        }
    }
}
