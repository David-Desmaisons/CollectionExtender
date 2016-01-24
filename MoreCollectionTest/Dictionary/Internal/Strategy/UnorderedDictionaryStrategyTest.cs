using MoreCollection.Dictionary.Internal;
using MoreCollection.Dictionary.Internal.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollectionTest.Dictionary.Internal.Strategy
{
    public class UnorderedDictionaryStrategyTest : DictionaryStrategyTest<MutableListDictionary<string, string>>
    {
        public UnorderedDictionaryStrategyTest()
        {
            DictionaryStrategy = new UnorderedDictionaryStrategy<string, string>(_Transition);
        }
    }
}
