using MoreCollection.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using NSubstitute;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableDictionaryTest
    {
        private readonly MutableDictionary<string, string> _DictionaryFourElements;
        private readonly MutableDictionary<string, string> _DictionaryThreeElements;
        private readonly IDictionaryStrategy _DictionarySwitcher;

        public MutableDictionaryTest()
        {
            _DictionarySwitcher = Substitute.For<IDictionaryStrategy>();

            _DictionaryFourElements = new MutableDictionary<string,string>
                (new Dictionary<string, string>(){  { "Key0", "Value0" }, { "Key1", "Value1" },
                                                    { "Key2", "Value2" }, { "Key3", "Value3" }}, 
                                                    _DictionarySwitcher);
            _DictionaryThreeElements = new MutableDictionary<string, string>
                (new Dictionary<string, string>() { { "Key0", "Value0" }, { "Key1", "Value1" }, 
                                                    { "Key2", "Value2" } },
                                                    _DictionarySwitcher);
        }

        [Fact]
        public void Constructor_ConstructEmptyDictionary()
        {
            var res = new MutableDictionary<string, string>(_DictionarySwitcher);
            res.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void Add_Return_SameObject()
        {
            var res = ((IMutableDictionary<string,string>)_DictionaryFourElements).AddMutable("Key4", "Value4");
            res.Should().BeSameAs(_DictionaryFourElements);
        }

        [Fact]
        public void Add_UpdateCollection()
        {
            var res = ((IMutableDictionary<string,string>)_DictionaryFourElements).AddMutable("Key4", "Value4");
            res.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3"),
                new KeyValuePair<string, string>("Key4", "Value4")}
                );
        }

        [Fact]
        public void Update_Return_SameObject()
        {
            var res = _DictionaryThreeElements.Update("Key2", "Value");
            res.Should().BeSameAs(_DictionaryThreeElements);
        }

        [Fact]
        public void Update_UpdateCollection()
        {
            var res = _DictionaryThreeElements.Update("Key2", "Value");
            res.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value")}
                );
        }

        [Fact]
        public void Remove_Call_OnDictionaryRemoved_OnSuccess()
        {
            bool res;
            var dicionary = _DictionaryFourElements.Remove("Key3", out res);
            _DictionarySwitcher.Received(1).CheckDictionaryRemoved(_DictionaryFourElements);
        }

        [Fact]
        public void Remove_DoNotCall_OnDictionaryRemoved_OnFailure()
        {
            bool res;
            var dicionary = _DictionaryFourElements.Remove("UnExisting", out res);
            _DictionarySwitcher.DidNotReceive().CheckDictionaryRemoved(_DictionaryFourElements);
        }

        [Fact]
        public void Remove_Return_True_IfKeyRemoved_IfElementNumberAboveLimit()
        {
            bool res;
            _DictionaryFourElements.Remove("Key3", out res);
            res.Should().BeTrue();
        }

        [Fact]
        public void Remove_Return_True_IfKeyRemoved_IfElementNumberCrossLimit()
        {
            bool res;
            _DictionaryThreeElements.Remove("Key2", out res);
            res.Should().BeTrue();
        }

        [Fact]
        public void Remove_Return_SameObject_IfElementNumberLimit()
        {
            bool res;
            var dicionary = _DictionaryThreeElements.Remove("Key4", out res);
            dicionary.Should().BeSameAs(_DictionaryThreeElements);
        }

        [Fact]
        public void Remove_Return_False_IfKeyNotRemoved_IfElementNumberLimit()
        {
            bool res;
            _DictionaryThreeElements.Remove("Key4", out res);
            res.Should().BeFalse();
        }

        [Fact]
        public void Remove_DoNotUpdateCollection_IfKeyNotRemoved_IfElementNumberLimit()
        {
            bool res;
            var dictionary = _DictionaryThreeElements.Remove("Key4", out res);
            dictionary.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2")}
              );
        }

         [Fact]
        public void ClearMutable_Call()
        {
            var dictionary = _DictionaryThreeElements.ClearMutable();
            _DictionarySwitcher.Received(1).GetEmpty<string, string>();
        }

        [Fact]
        public void MutableSortedDictionary_Throw_Exception_IfElementIsNotComparable()
        {
            var dictionarySwitcher = Substitute.For<IDictionaryStrategy>();
            var res = new MutableSortedListDictionary<object, string>(dictionarySwitcher);
            res.Add(new Object(), "aaaa");
            Action Do = () => res.Add(new Object(), "bbb");
            Do.ShouldThrow<Exception>();
        }
    }
}
