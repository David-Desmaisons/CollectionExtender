using CollectionExtender.Dictionary.Internal;
using CollectionExtenderTest.TestInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtenderTest.Dictionary.Internal
{
    public class ListDictionaryTest : DictionaryTest
    {
        public ListDictionaryTest()
        {
            _dictionary = new ListDictionary<string, string>();
        }
    }
}
