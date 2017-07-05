using MoreCollection.Dictionary.Internal;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using NSubstitute;
using MoreCollection.Dictionary.Internal.Strategy;
using System;

namespace MoreCollectionTest.Dictionary.Internal
{
    [Collection("Changing Default static Dictionary stategy")]
    public abstract class MutableMiddleDictionaryTest : IDisposable
    {
        private readonly IMutableDictionary<string, string> _DictionaryTwoElements;
        private readonly IDictionaryStrategy _DictionarySwitcher;

        public MutableMiddleDictionaryTest()
        { 
            _DictionarySwitcher = Substitute.For<IDictionaryStrategy>();
            DictionaryStrategyFactory<string>.Strategy = _DictionarySwitcher;
           _DictionaryTwoElements = Get( new Dictionary<string, string>() { { "Key0", "Value0" }, { "Key1", "Value1" } });
        }

        public void Dispose()
        {
            DictionaryStrategyFactory<string>.Strategy = DictionaryStrategyFactory<string>.GetStrategy();
        }

        protected abstract IMutableDictionary<string, string> Get(IDictionary<string, string> Original);

        protected abstract IMutableDictionary<string, string> GetEmpty();

        [Fact]
        public void Constructor_ConstructEmptyDictionary()
        {
            var res = GetEmpty();
            res.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void AddMutable_Call_DictionarySwitcher_Add()
        {
            _DictionaryTwoElements.AddMutable("Key2", "Value2");
            _DictionarySwitcher.Received(1).Add(_DictionaryTwoElements, "Key2", "Value2");
        }

        [Fact]
        public void Update_Call_DictionarySwitcher_Update()
        {
            var res = _DictionaryTwoElements.Update("Key2", "Value");
            _DictionarySwitcher.Received(1).Update(_DictionaryTwoElements, "Key2", "Value");
        }

        [Fact]
        public void Remove_Call_DictionarySwitcher_Remove()
        {
            bool res;
            var dicionary = _DictionaryTwoElements.Remove("Key1", out res);
            _DictionarySwitcher.Received(1).Remove(_DictionaryTwoElements, "Key1", out res);
        }

        [Fact]
        public void Clear_Call_DictionarySwitcher_Clear()
        {
            _DictionaryTwoElements.ClearMutable();
            _DictionarySwitcher.Received(1).GetEmpty<string, string>();
        }
    }
}
