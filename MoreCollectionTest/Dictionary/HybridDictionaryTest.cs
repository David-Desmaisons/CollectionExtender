using MoreCollection.Dictionary;
using MoreCollection.Dictionary.Internal.Strategy;
using MoreCollectionTest.TestInfra;
using Xunit;

namespace MoreCollectionTest.Dictionary
{
    public class HybridDictionaryTest : DictionaryTest
    {
        public HybridDictionaryTest()
        {
            DictionaryStrategyFactory<string>.Strategy = DictionaryStrategyFactory<string>.GetStrategy();
            _dictionary = new HybridDictionary<string, string>();
        }
    }
}
