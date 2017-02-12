using MoreCollection.Dictionary.Internal;
using System.Collections.Generic;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableListDictionaryTest : MutableMiddleDictionaryTest
    {
        protected override IMutableDictionary<string, string> Get(IDictionary<string, string> Original)
        {
            return new MutableListDictionary<string, string>(Original);
        }

        protected override IMutableDictionary<string, string> GetEmpty()
        {
            return new MutableListDictionary<string, string>();
        }
    }
}
