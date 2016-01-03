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
    public class SortedDictionaryLifeCycleStrategyTest : DictionaryLifeCycleStrategyTest
    {
        public SortedDictionaryLifeCycleStrategyTest()
        {
            _DictionaryLifeCycleStrategy = new SortedDictionaryLifeCycleStrategy<string, string>(2);
        }

        [Fact]
        public void Add_OnOneElementDictionary_Returns_SortedList_With_CorrectInformation()
        {
            _DictionaryLifeCycleStrategy.Add(ref _OneElement, "Key0", "Value0");
            _OneElement.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key0", "Value0"), 
                new KeyValuePair<string, string>("Key1", "Value1") });
            _OneElement.Should().BeOfType<SortedList<string, string>>();
        }

        [Fact]
        public void Remove_OnDictionaryWithThresholdElements_Returns_SortedList_OnSuccess()
        {
            var res = _DictionaryLifeCycleStrategy.Remove(ref _ThreeElements, "Key1");
            res.Should().BeTrue();
            _ThreeElements.AsEnumerable().Should().BeEquivalentTo(new[] {   
                new KeyValuePair<string, string>("Key3", "Value3"),
                new KeyValuePair<string, string>("Key2", "Value2")});
            _ThreeElements.Should().BeOfType<SortedList<string, string>>();
        }

        [Fact]
        public void Update_OnOneElementDictionary_Returns_SameDictionary_OnUpdate()
        {
            _DictionaryLifeCycleStrategy.Update(ref _OneElement, "Key0", "Value0");
            _OneElement.Should().BeOfType<SortedList<string, string>>();
            _OneElement.AsEnumerable().Should().BeEquivalentTo(new[] { 
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key0", "Value0")});
        }
    }
}
