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

        protected override IMutableDictionary<string, string> Get(IDictionary<string, string> Original)
        {
            return new MutableSortedListDictionary<string, string>(Original);
        }

        protected override IMutableDictionary<string, string> GetEmpty()
        {
            return new MutableSortedListDictionary<string, string>();
        }
    }
}
