using MoreCollection.Dictionary.Internal;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using NSubstitute;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableSingleDictionaryTest
    {
        private readonly IMutableDictionary<string, string> _DictionaryNoElement;
        private readonly IMutableDictionary<string, string> _DictionaryOneElement;
        private readonly IDictionaryStrategy _DictionarySwitcher;

        public MutableSingleDictionaryTest()
        {
            _DictionarySwitcher = Substitute.For<IDictionaryStrategy>();

            _DictionaryNoElement = new MutableSingleDictionary<string, string>( _DictionarySwitcher);

            var Dictionary = new Dictionary<string, string>(){{"Key0", "Value0"}};
            _DictionaryOneElement = new MutableSingleDictionary<string, string>( Dictionary, _DictionarySwitcher);
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
        public void Add_Call_DictionarySwitcher_Add()
        {
            var res = _DictionaryOneElement.AddMutable("Key1", "Value1");
            _DictionarySwitcher.Received(1).GetIntermediateCollection(_DictionaryOneElement);
        }

        [Fact]
        public void ClearMutable_Return_SameObject()
        {
            var res = _DictionaryOneElement.ClearMutable();
            res.Should().BeSameAs(_DictionaryOneElement);
        }

        [Fact]
        public void ClearMutable_EmptyCollection()
        {
            _DictionaryOneElement.ClearMutable();
            _DictionaryOneElement.Should().BeEmpty();
        }
    }
}
