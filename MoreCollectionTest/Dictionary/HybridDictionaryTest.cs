using MoreCollection.Dictionary;
using MoreCollection.Dictionary.Internal;
using MoreCollectionTest.TestInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
