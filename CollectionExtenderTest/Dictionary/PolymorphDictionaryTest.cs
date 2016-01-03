using CollectionExtender.Dictionary;
using CollectionExtender.Dictionary.Internal;
using CollectionExtenderTest.TestInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtenderTest.Dictionary
{
    public class PolymorphDictionaryTest : DictionaryTest
    {
        public PolymorphDictionaryTest()
        {
            _dictionary = new PolymorphDictionary<string, string>();
        }
    }
}
