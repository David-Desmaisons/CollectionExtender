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
    public abstract class MutableMiddleDictionaryTest
    {
        private IMutableDictionary<string, string> _DictionaryTwoElements;
        private IMutableDictionary<string, string> _DictionaryThreeElements;

        public MutableMiddleDictionaryTest()
        {
            _DictionaryTwoElements = Get( new Dictionary<string, string>() { { "Key0", "Value0" }, { "Key1", "Value1" } }, 3);
            _DictionaryThreeElements = Get( new Dictionary<string, string>() { { "Key0", "Value0" }, { "Key1", "Value1" }, 
                                                    { "Key2", "Value2" } }, 3);
        }

        protected abstract IMutableDictionary<string, string> Get(IDictionary<string, string> Original, int Transition);

        protected abstract IMutableDictionary<string, string> GetEmpty();

        [Fact]
        public void Constructor_ConstructEmptyDictionary()
        {
            var res = GetEmpty();
            res.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void Add_Return_SameObject_IfBelowLimit()
        {
            var res = _DictionaryTwoElements.AddMutable("Key2", "Value2");
            res.Should().BeSameAs(_DictionaryTwoElements);
        }

        [Fact]
        public void Add_UpdateCollection_IfBelowLimit()
        {
            var res = _DictionaryTwoElements.AddMutable("Key2", "Value2");
            res.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2")});
        }

        [Fact]
        public void Add_Return_NewObject_IfAboveLimit()
        {
            var res = _DictionaryThreeElements.AddMutable("Key3", "Value3");
            res.Should().BeOfType<MutableDictionary<string, string>>();
        }

        [Fact]
        public void Add_UpdateCollection_IfAboveLimit()
        {
            var res = _DictionaryThreeElements.AddMutable("Key3", "Value3");
            res.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")});
        }

        [Fact]
        public void Update_Return_NewObject_IfAboveLimit()
        {
            var res = _DictionaryThreeElements.Update("Key3", "Value3");
            res.Should().BeOfType<MutableDictionary<string, string>>();
        }

        [Fact]
        public void Update_UpdateCollection_IfAboveLimit()
        {
            var res = _DictionaryThreeElements.Update("Key3", "Value3");
            res.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")});
        }

        [Fact]
        public void Update_Return_SameObject_IfUpdated()
        {
            var res = _DictionaryThreeElements.Update("Key2", "Value");
            res.Should().BeSameAs(_DictionaryThreeElements);
        }

        [Fact]
        public void Update_UpdateCollection_IfUpdated()
        {
            var res = _DictionaryThreeElements.Update("Key2", "Value");
            res.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value")}
                );
        }

        [Fact]
        public void Remove_Return_NewObject_IfElementNumberCrossLimit()
        {
            bool res;
            var dicionary = _DictionaryTwoElements.Remove("Key1", out res);
            dicionary.Should().BeOfType<MutableSingleDictionary<string, string>>();
        }

        [Fact]
        public void Remove_Return_True_IfKeyRemoved_IfElementNumberCrossLimit()
        {
            bool res;
            _DictionaryTwoElements.Remove("Key1", out res);
            res.Should().BeTrue();
        }

        [Fact]
        public void Remove_UpdateCollection_IfElementNumberCrossLimit()
        {
            bool res;
            var dictionary = _DictionaryTwoElements.Remove("Key1", out res);
            dictionary.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0")}
              );
        }

        [Fact]
        public void Remove_Return_SameObject_IfElementNumberDoNotCrossLimit_KeyNotFound()
        {
            bool res;
            var dicionary = _DictionaryTwoElements.Remove("Key2", out res);
            dicionary.Should().BeSameAs(_DictionaryTwoElements);
            res.Should().BeFalse();
        }         

        [Fact]
        public void Remove_Return_SameObject_IfElementNumberAboveLimit()
        {
            bool res;
            var dicionary = _DictionaryThreeElements.Remove("Key2", out res);
            dicionary.Should().BeSameAs(_DictionaryThreeElements);
            res.Should().BeTrue();
        }

        [Fact]
        public void Remove_UpdateCollection_IfElementNumberAboveLimit()
        {
            bool res;
            var dictionary = _DictionaryThreeElements.Remove("Key2", out res);
            dictionary.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"),
                new KeyValuePair<string, string>("Key1", "Value1")}
              );
        }
    }
}
