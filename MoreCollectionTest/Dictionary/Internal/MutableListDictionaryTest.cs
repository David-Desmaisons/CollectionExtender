using MoreCollection.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableListDictionaryTest : MutableMiddleDictionaryTest
    {

        protected override IMutableDictionary<string, string> Get(IDictionary<string, string> Original, int Transition)
        {
            return new MutableListDictionary<string, string>(Original, Transition);
        }

        protected override IMutableDictionary<string, string> GetEmpty()
        {
            return new MutableListDictionary<string, string>();
        }
    }
}
