using MoreCollection.Composed;
using FluentAssertions;
using Xunit;
using System;
using System.Collections.Generic;

namespace MoreCollectionTest.Composed
{
    public class SimpleLookUpTest
    {
        private readonly SimpleLookUp<string, int> _Lookup;
        private readonly SimpleLookUp<string, int> _LookupWithData;

        public SimpleLookUpTest()
        {
            _Lookup = new SimpleLookUp<string, int>();
            _LookupWithData = new SimpleLookUp<string, int>();
            _LookupWithData.Add("ab", 1);
            _LookupWithData.Add("bc", 1);
        }

        [Fact]
        public void New_IsEmpty()
        {
            _Lookup.Should().BeEmpty();
        }

        [Theory]
        [InlineData("a", 1)]
        [InlineData("b", 1)]
        [InlineData("c", 34)]
        public void Add_AddAnElement(string key, int value) 
        {
            _Lookup.Add(key, value);
            _Lookup[key].Should().BeEquivalentTo(new[] {value});
        }

        [Theory]
        [InlineData("ab", 2)]
        [InlineData("ab", 3)]
        [InlineData("ab", 10)]
        public void Add_AppendToListAnElement(string key, int value)
        {
            _LookupWithData.Add(key, value);
            _LookupWithData[key].Should().BeEquivalentTo(new[] { 1, value });
        }

        [Theory]
        [InlineData("ab", true)]
        [InlineData("bc", true)]
        [InlineData("cd", false)]
        [InlineData("", false)]
        public void Contains_Works(string key, bool value)
        {
            _LookupWithData.Contains(key).Should().Be(value);
        }

        [Theory]
        [InlineData("ab", 1, true)]
        [InlineData("ab", 2, false)]
        [InlineData("bc", 1, true)]
        [InlineData("cd", 1, false)]
        [InlineData("", 1, false)]
        public void Remove_Works(string key, int value, bool boolvalue)
        {
            _LookupWithData.Remove(key, value).Should().Be(boolvalue);
            if (boolvalue)
                _LookupWithData.Contains(key).Should().BeFalse();
        }

        [Fact]
        public void Item_ThrowException_WhenKeyDoesNotExist()
        {
            IEnumerable<int> res = null;
            Action Do = () => res = _LookupWithData["notFound"];
            Do.Should().Throw<KeyNotFoundException>();
        }
    }
}
