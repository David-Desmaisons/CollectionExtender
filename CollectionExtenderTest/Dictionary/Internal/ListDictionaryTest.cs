using CollectionExtender.Dictionary.Internal;
using CollectionExtenderTest.TestInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace CollectionExtenderTest.Dictionary.Internal
{
    public class ListDictionaryTest : DictionaryTest
    {
        public ListDictionaryTest()
        {
            _dictionary = new ListDictionary<string, string>();
        }

        [Fact]
        public void Constructor_WithDictionary_CreateDictiionaryWithSameKey()
        {
            var dict = new Dictionary<string, string>(){
                {"a", "A"}, {"b", "B"}, {"c", "C"}
            };
            var target = new ListDictionary<string, string>(dict);
            target.AsEnumerable().Should().BeEquivalentTo(dict);
        }
    }
}
