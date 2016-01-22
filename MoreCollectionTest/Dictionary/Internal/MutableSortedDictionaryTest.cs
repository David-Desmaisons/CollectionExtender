using MoreCollection.Dictionary.Internal;
using System.Collections.Generic;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableSortedDictionaryTest : MutableMiddleDictionaryTest
    {
        protected override IMutableDictionary<string, string> Get(IDictionary<string, string> Original, int Transition)
        {
            return new MutableSortedDictionary<string, string>(Original, Transition);
        }

        protected override IMutableDictionary<string, string> GetEmpty()
        {
            return new MutableSortedDictionary<string, string>();
        }
    }
}
