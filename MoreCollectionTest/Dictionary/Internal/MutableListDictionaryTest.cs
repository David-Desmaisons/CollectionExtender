using MoreCollection.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NSubstitute;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableListDictionaryTest : MutableMiddleDictionaryTest
    {
        protected override IMutableDictionary<string, string> Get(IDictionary<string, string> Original, IDictionaryStrategy<string, string> strategy)
        {
            return new MutableListDictionary<string, string>(Original, strategy);
        }

        protected override IMutableDictionary<string, string> GetEmpty()
        {
            return new MutableListDictionary<string, string>(new OrderedDictionaryStrategy<string, string>(4));
        }
    }
}
