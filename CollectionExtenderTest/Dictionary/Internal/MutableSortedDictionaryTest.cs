using CollectionExtender.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace CollectionExtenderTest.Dictionary.Internal
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
