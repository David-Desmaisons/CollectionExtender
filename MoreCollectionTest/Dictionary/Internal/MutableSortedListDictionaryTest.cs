using System.Collections.Generic;
using NSubstitute;
using MoreCollection.Dictionary.Internal;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableSortedListDictionaryTest : MutableMiddleDictionaryTest
    {
        private readonly IDictionaryStrategy _DictionarySwitcher;

        public MutableSortedListDictionaryTest()
        {
            _DictionarySwitcher = Substitute.For<IDictionaryStrategy>();
        }

        protected override IMutableDictionary<string, string> Get(IDictionary<string, string> Original,  IDictionaryStrategy strategy)
        {
            return new MutableSortedListDictionary<string, string>(Original, strategy);
        }

        protected override IMutableDictionary<string, string> GetEmpty()
        {
            return new MutableSortedListDictionary<string, string>(new OrderedDictionaryStrategy(4));
        }
    }
}
