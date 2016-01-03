using CollectionExtender.Dictionary.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace CollectionExtenderTest.Dictionary.Internal
{
    public abstract class DictionaryLifeCycleStrategyTest
    {
        protected IDictionaryLifeCycleStrategy<string, string> _DictionaryLifeCycleStrategy;
        protected IDictionary<string, string> _Null = null;
        protected IDictionary<string, string> _OneElement = 
            new Dictionary<string, string>() { { "Key1", "Value1"}};
        protected IDictionary<string, string> _TwoElements = 
            new ListDictionary<string, string>(){  {"Key1","Value1"},  {"Key2","Value2"}};
        protected IDictionary<string, string> _ThreeElements = 
            new Dictionary<string, string>(){  {"Key1","Value1"}, {"Key2","Value2"}, {"Key3","Value3"}};
        protected IDictionary<string, string> _FourElements = 
            new Dictionary<string, string>(){ {"Key1","Value1"}, {"Key2","Value2"},
                                              {"Key3","Value3"}, {"Key4","Value4"}};
        [Fact]
        public void Add_OnEmptyDictionary_Returns_SingleElementCollection_With_CorrectInformation()
        {
            _DictionaryLifeCycleStrategy.Add(ref _Null, "Key0", "Value0");
            _Null.AsEnumerable().Should().Equal(new[] { new KeyValuePair<string, string>("Key0", "Value0") });
            _Null.Should().BeOfType<SingleDictionary<string, string>>();
        }

        [Fact]
        public void Add_WithNullKey_ThrowArgumentNullException()
        {
            Action Do = () => _DictionaryLifeCycleStrategy.Add(ref _OneElement, null, "Value0");
            Do.ShouldThrow<ArgumentNullException>();
        }
   
        [Fact]
        public void Add_OnFullMiddleDictionary_Returns_Dictionary_With_CorrectInformation()
        {
            _DictionaryLifeCycleStrategy.Add(ref _TwoElements, "Key0", "Value0");
            _TwoElements.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"), 
                new KeyValuePair<string, string>("Key1", "Value1"), 
                new KeyValuePair<string, string>("Key2", "Value2") });
            _TwoElements.Should().BeOfType<Dictionary<string, string>>();
        }

        [Fact]
        public void Remove_OnNullDictionary_Returns_Null_AndFalse()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _Null, "Key1");
            res.Should().BeFalse();
            _Null.Should().BeNull();
        }

        [Fact]
        public void Remove_WithNullKey_ThrowArgumentNullException()
        {
            Action Do = () => _DictionaryLifeCycleStrategy.Remove(ref _OneElement, null);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Remove_OnDictionaryWithOneElement_Returns_Null_OnSuccess()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _OneElement, "Key1");
            res.Should().BeTrue();
            _OneElement.Should().BeNull();
        }

        [Fact]
        public void Remove_OnDictionaryWithOneElement_DoNotChangeCollection_OnFailure()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _OneElement, "Key2");
            res.Should().BeFalse();
            _OneElement.AsEnumerable().Should().Equal(new[] { 
                new KeyValuePair<string, string>("Key1", "Value1") });
        }

        [Fact]
        public void Remove_OnDictionaryWithTwoElement_Returns_SingleDictionary_OnSuccess()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _TwoElements, "Key1");
            res.Should().BeTrue();
            _TwoElements.AsEnumerable().Should().Equal(new[] { new KeyValuePair<string, string>("Key2", "Value2") });
            _TwoElements.Should().BeOfType<SingleDictionary<string, string>>();
        }

        [Fact]
        public void Remove_OnDictionaryWithTwoElement_DoNotChangeCollection_OnFailure()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _TwoElements, "Key3");
            res.Should().BeFalse();
            _TwoElements.AsEnumerable().Should().BeEquivalentTo(new[] {   
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2")});
        }

        [Fact]
        public void Remove_OnDictionaryWithThresholdElement_DoNotChangeCollection_OnFailure()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _ThreeElements, "Key4");
            res.Should().BeFalse();
            _ThreeElements.AsEnumerable().Should().BeEquivalentTo(new[] {   
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")});
        }

        [Fact]
        public void Remove_OnDictionaryBeyondLimit_WorkAsExpected_Sucess()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _FourElements, "Key1");
            res.Should().BeTrue();
            _FourElements.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3"),
                new KeyValuePair<string, string>("Key4", "Value4")});
        }

        [Fact]
        public void Remove_OnDictionaryBeyondLimit_WorkAsExpected_Failure()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _FourElements, "Key5");
            res.Should().BeFalse();
            _FourElements.AsEnumerable().Should() .BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3"),
                new KeyValuePair<string, string>("Key4", "Value4")});
        }

        [Fact]
        public void Update_WithNullDictionary_ThrowException()
        {
            Action Do = () => _DictionaryLifeCycleStrategy.Update(ref _OneElement, null, "Value0");
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Update_OnNullDictionary_Returns_SingleDictionary()
        {
            _DictionaryLifeCycleStrategy.Update(ref _Null, "Key0", "Value0");
            _Null.Should().BeOfType<SingleDictionary<string, string>>();
            _Null.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0")});
        }

        [Fact]
        public void Update_OnOneElementDictionary_Returns_SameDictionary_OnUpdate()
        {
            _DictionaryLifeCycleStrategy.Update(ref _OneElement, "Key1", "Value10");
            _OneElement.Should().BeOfType<Dictionary<string, string>>();
            _OneElement.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key1", "Value10")});
        }

        [Fact]
        public void Update_OnTwoElementsDictionary_Returns_SameDictionary_OnUpdate()
        {
            _DictionaryLifeCycleStrategy.Update(ref _TwoElements, "Key1", "Value10");
            _TwoElements.Should().BeOfType<ListDictionary<string, string>>();
            _TwoElements.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key1", "Value10")});
        }

        [Fact]
        public void Update_OnTwoElementsDictionary_Returns_Dictionary_OnAdd()
        {
            _DictionaryLifeCycleStrategy.Update(ref _TwoElements, "Key3", "Value3");
            _TwoElements.Should().BeOfType<Dictionary<string, string>>();
            _TwoElements.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3"),
                new KeyValuePair<string, string>("Key1", "Value1")});
        }

        [Fact]
        public void Update_OnFourElementsDictionary_Returns_Dictionary_OnAdd()
        {
            _DictionaryLifeCycleStrategy.Update(ref _FourElements, "Key30", "Value30");
            _FourElements.Should().BeOfType<Dictionary<string, string>>();
            _FourElements.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key4", "Value4"),
                new KeyValuePair<string, string>("Key30", "Value30"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3"),
                new KeyValuePair<string, string>("Key1", "Value1")});
        }

        [Fact]
        public void Update_OnFourElementsDictionary_Returns_Dictionary_OnUpdate()
        {
            _DictionaryLifeCycleStrategy.Update(ref _FourElements, "Key4", "Value40");
            _FourElements.Should().BeOfType<Dictionary<string, string>>();
            _FourElements.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key4", "Value40"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3"),
                new KeyValuePair<string, string>("Key1", "Value1")});
        }
    }
}
