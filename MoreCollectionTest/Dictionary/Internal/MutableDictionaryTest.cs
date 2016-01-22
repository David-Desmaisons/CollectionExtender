using MoreCollection.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableDictionaryTest
    {
        private IMutableDictionary<string, string> _DictionaryFourElements;
        private IMutableDictionary<string, string> _DictionaryThreeElements;

        public MutableDictionaryTest()
        {
            _DictionaryFourElements = new MutableDictionary<string,string>
                (new Dictionary<string, string>(){  { "Key0", "Value0" }, { "Key1", "Value1" },
                                                    { "Key2", "Value2" }, { "Key3", "Value3" }}, 
                                                    typeof(MutableListDictionary<string,string>),
                                                    2);
            _DictionaryThreeElements = new MutableDictionary<string, string>
                (new Dictionary<string, string>() { { "Key0", "Value0" }, { "Key1", "Value1" }, 
                                                    { "Key2", "Value2" } },
                                                    typeof(MutableListDictionary<string, string>), 
                                                    2);
        }

        [Fact]
        public void Constructor_ConstructEmptyDictionary()
        {
            var res = new MutableDictionary<string, string>(typeof(MutableListDictionary<string,string>));
            res.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void Add_Return_SameObject()
        {
            var res = _DictionaryFourElements.AddMutable("Key4", "Value4");
            res.Should().BeSameAs(_DictionaryFourElements);
        }

        [Fact]
        public void Add_UpdateCollection()
        {
            var res = _DictionaryFourElements.AddMutable("Key4", "Value4");
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
        public void Remove_Return_SameObject_IfElementNumberAboveLimit()
        {
            bool res;
            var dicionary = _DictionaryFourElements.Remove("Key3", out res);
            dicionary.Should().BeSameAs(_DictionaryFourElements);
        }

        [Fact]
        public void Remove_Return_True_IfKeyRemoved_IfElementNumberAboveLimit()
        {
            bool res;
            _DictionaryFourElements.Remove("Key3", out res);
            res.Should().BeTrue();
        }

        [Fact]
        public void Remove_UpdateCollection_IfElementNumberAboveLimit()
        {
            bool res;
            var dictionary = _DictionaryFourElements.Remove("Key3", out res);
            dictionary.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2")}
              );
        }

        [Fact]
        public void Remove_Return_NewObject_IfElementNumberCrossLimit()
        {
            bool res;
            var dicionary = _DictionaryThreeElements.Remove("Key2", out res);
            dicionary.Should().BeOfType<MutableListDictionary<string,string>>();
        }

        [Fact]
        public void Remove_Return_True_IfKeyRemoved_IfElementNumberCrossLimit()
        {
            bool res;
            _DictionaryThreeElements.Remove("Key2", out res);
            res.Should().BeTrue();
        }

        [Fact]
        public void Remove_UpdateCollection_IfElementNumberCrossLimit()
        {
            bool res;
            var dictionary = _DictionaryThreeElements.Remove("Key2", out res);
            dictionary.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1")}
              );
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
        public void MutableSortedDictionary_Throw_Exception_IfElementIsNotComparable()
        {
            var res = new MutableSortedDictionary<object, string>();
            res.Add(new Object(), "aaaa");
            Action Do = () => res.Add(new Object(), "bbb");
            Do.ShouldThrow<Exception>();
        }
    }
}
