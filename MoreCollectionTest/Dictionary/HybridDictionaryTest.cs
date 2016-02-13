using MoreCollection.Dictionary;
using MoreCollectionTest.TestInfra;
using Xunit;

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
