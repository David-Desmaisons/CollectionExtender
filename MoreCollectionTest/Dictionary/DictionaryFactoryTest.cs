using FluentAssertions;
using MoreCollection.Dictionary;
using MoreCollection.Dictionary.Internal;
using System;
using System.Collections.Generic;
using Xunit;

namespace MoreCollectionTest.Dictionary 
{
    public class DictionaryFactoryTest 
    {
        [Theory]
        [InlineData(0, typeof(SortedList<string, object>))]
        [InlineData(1, typeof(SortedList<string, object>))]
        [InlineData(8, typeof(SortedList<string, object>))]
        [InlineData(9, typeof(SortedList<string, object>))]
        [InlineData(10, typeof(Dictionary<string, object>))]
        [InlineData(100, typeof(Dictionary<string, object>))]
        [InlineData(1000, typeof(Dictionary<string,object>))]
        public void Get_returns_correct_dictionary_type_comparable(int size, Type expectedType) 
        {
            CheckGet<string>(size, expectedType);
        }

        [Theory]
        [InlineData(0, typeof(ListDictionary<DictionaryFactoryTest, object>))]
        [InlineData(1, typeof(ListDictionary<DictionaryFactoryTest, object>))]
        [InlineData(8, typeof(ListDictionary<DictionaryFactoryTest, object>))]
        [InlineData(9, typeof(ListDictionary<DictionaryFactoryTest, object>))]
        [InlineData(10, typeof(Dictionary<DictionaryFactoryTest, object>))]
        [InlineData(100, typeof(Dictionary<DictionaryFactoryTest, object>))]
        [InlineData(1000, typeof(Dictionary<DictionaryFactoryTest, object>))]
        public void Get_returns_correct_dictionary_type_not_comparable(int size, Type expectedType)
        {
            CheckGet<DictionaryFactoryTest>(size, expectedType);
        }


        public void CheckGet<TKey>(int expectedSize, Type expectedType) 
        {
            var res = DictionaryFactory.Get<TKey, object>(expectedSize);
            res.Should().BeOfType(expectedType);
        }
    }
}
