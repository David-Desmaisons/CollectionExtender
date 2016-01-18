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
    public class MutableSingleDictionaryTest
    {
        private IMutableDictionary<string, string> _DictionaryNoElement;
        private IMutableDictionary<string, string> _DictionaryOneElement;

        public MutableSingleDictionaryTest()
        {
            _DictionaryNoElement =
                new MutableSingleDictionary<string, string>(typeof(MutableListDictionary<string, string>));

            var Dictionary = new Dictionary<string, string>(){{"Key0", "Value0"}};
            _DictionaryOneElement =
                new MutableSingleDictionary<string, string>(Dictionary, typeof(MutableListDictionary<string, string>));
        }

        [Fact]
        public void Constructor_ConstructEmptyDictionary()
        {
            _DictionaryNoElement.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void Add_Return_SameObject_IfElementNumberBelowLimit()
        {
            var res = _DictionaryNoElement.AddMutable("Key0", "Value0");
            res.Should().BeSameAs(res);
        }

        [Fact]
        public void Add_UpdateCollection_IfElementNumberBelowLimit()
        {
            var res = _DictionaryNoElement.AddMutable("Key0", "Value0");
            _DictionaryNoElement.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0")} );
        }

        [Fact]
        public void Add_Return_NewObject_IfElementNumberAboveLimit()
        {
            var res = _DictionaryOneElement.AddMutable("Key1", "Value1");
            res.Should().BeOfType<MutableListDictionary<string, string>>();
        }

        [Fact]
        public void Add_UpdateCollection_IfElementNumberAboveLimit()
        {
            var res = _DictionaryOneElement.AddMutable("Key1", "Value1");
            res.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1")}
                );
        }
    }
}
