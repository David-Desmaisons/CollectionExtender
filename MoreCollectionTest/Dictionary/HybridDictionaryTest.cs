using MoreCollection.Dictionary;
using MoreCollectionTest.TestInfra;

namespace MoreCollectionTest.Dictionary
{
    public class HybridDictionaryTest : DictionaryTest
    {
        public HybridDictionaryTest()
        {
            _dictionary = new HybridDictionary<string, string>();
        }
    }
}
