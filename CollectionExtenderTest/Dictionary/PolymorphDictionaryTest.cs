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
    public class PolymorphDictionaryTest : DictionaryTest
    {
        public PolymorphDictionaryTest()
        {
            _dictionary = new PolymorphDictionary<string, string>();
        }
    }
}
