using MoreCollection.Dictionary.Internal;
using MoreCollectionTest.TestInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class SingleDictionaryTest
    {
        private SingleDictionary<string, string> _dictionary;
        public SingleDictionaryTest()
        {
            var Dictionary = new Dictionary<string, string>() { { "key", "value" } };
            _dictionary = new SingleDictionary<string, string>(Dictionary);
        }

        [Fact]
        public void Construct_WithOversizedDictionaryParameter_ThrowException()
        {
            var dict = new Dictionary<string, string>() { 
                    { "key2", "value2" }, 
                    { "key1", "value1" } };
            SingleDictionary<string, string> target =null;
            Action Do = () => { target = new SingleDictionary<string, string>(dict); };
            Do.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Construct_WithEmptyDictionaryParameter_IsEmpty()
        {
            var dict = new Dictionary<string, string>();
            var target = new SingleDictionary<string, string>(dict);
            target.AsEnumerable().Should().BeEmpty();
        }


        [Fact]
        public void ContainsKey_NoneExistingKey_ReturnFalse()
        {
            var ok = _dictionary.ContainsKey("toto");
            ok.Should().BeFalse();
        }

        [Fact]
        public void ContainsKey_ExistingKey_ReturnTrue()
        {
            var ok = _dictionary.ContainsKey("key");
            ok.Should().BeTrue();
        }

        [Fact]
        public void Add_ThrowsException_WhenKeyIsNull()
        {
            _dictionary = new SingleDictionary<string, string>();
            Action Do = () => _dictionary.Add(null, "value2");
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Add_ThrowsNotImplementedException_WhenDictionaryIsNotEmpty()
        {
           Action Do = () => _dictionary.Add("key2","value2");
           Do.ShouldThrow<NotImplementedException>();
        }

        [Fact]
        public void Add_AddKeyValue_WhenDictionaryIsEmpty()
        {
            _dictionary = new SingleDictionary<string, string>();
            _dictionary.Add( new  KeyValuePair<string, string> ( "key2", "value2"));
            _dictionary.AsEnumerable().Should().BeEquivalentTo(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("key2", "value2")});
        }

        [Fact]
        public void AddKeyValuePair_ThrowsNotImplementedException()
        {
            Action Do = () => _dictionary.Add(new KeyValuePair<string, string> ("key2", "value2"));
            Do.ShouldThrow<NotImplementedException>();
        }

        [Fact]
        public void Remove_ReturnsTrue_WhenKeyFound()
        {
            var res = _dictionary.Remove("key");
            res.Should().BeTrue();
            _dictionary.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void Remove_ReturnsFalse_WhenKeyNotFound()
        {
            var res = _dictionary.Remove("key2");
            res.Should().BeFalse();
            _dictionary.AsEnumerable().Should().BeEquivalentTo(new[]{ new KeyValuePair<string, string>("key","value")});
        }

        [Fact]
        public void Remove_ReturnsFalse_WhenKeyNotFound_WhenEmpty()
        {
            _dictionary = new SingleDictionary<string, string>();
            var res = _dictionary.Remove("Key");
            res.Should().BeFalse();
            _dictionary.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void RemoveKeyValuePair_ReturnsFalse_WhenKeyNotFound()
        {
            var res = _dictionary.Remove(new KeyValuePair<string, string>("key2", "value2"));
            res.Should().BeFalse();
        }

        [Fact]
        public void RemoveKeyValuePair_ReturnsFalse_WhenKeyFoundButNotValue()
        {
            var res = _dictionary.Remove(new KeyValuePair<string, string>("key", "value2"));
            res.Should().BeFalse();
        }

        [Fact]
        public void RemoveKeyValuePair_ReturnsTrue_WhenKeyAndValueFound()
        {
            var res = _dictionary.Remove(new KeyValuePair<string, string>("key", "value"));
            res.Should().BeTrue();
            _dictionary.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void Clear_EmptyCollection()
        {
            _dictionary.Clear();
            _dictionary.AsEnumerable().Should().BeEmpty();
        }
        
        [Fact]
        public void IsReadOnly_ThrowsNotImplementedException()
        {
            var res = _dictionary.IsReadOnly;
            res.Should().BeFalse();
        }

        [Fact]
        public void Indexer_Get_ThrowExceptionIfKeyNotFound()
        {
            string value;
            Action Do = () => value = _dictionary["key0"];
            Do.ShouldThrow<KeyNotFoundException>();
        }

        [Fact]
        public void Indexer_Get_ReturnValueIfKeyFound()
        {
            var value = _dictionary["key"];
            value.Should().Be("value");
        }

        [Theory]
        [InlineData("V2")]
        [InlineData("newvalue")]
        [InlineData("newvalue2")]
        [InlineData("J0")]
        public void Indexer_Set_ExistingKey_UpdateCorrespondingEntry(string Value)
        {
            _dictionary["key"] = Value;
            _dictionary["key"].Should().Be(Value);
        }

        [Fact]
        public void Indexer_Set_ThrowExceptionIfNewKey()
        {
            Action Do = () => _dictionary["key1"] = "Value1";
            Do.ShouldThrow<NotImplementedException>();
        }

        [Fact]
        public void CopyTo_Null_ThrowException()
        {
            Action Do = () => _dictionary.CopyTo(null,0);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CopyTo_NegativeIndex_ThrowException()
        {
            var array = new KeyValuePair<string, string>[0];
            Action Do = () => _dictionary.CopyTo(array, -1);
            Do.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CopyTo_WrongIndex_ThrowException()
        {
            var array = new KeyValuePair<string, string>[1];
            Action Do = () => _dictionary.CopyTo(array, 2);
            Do.ShouldThrow<IndexOutOfRangeException>();
        }

        [Fact]
        public void CopyTo_ShouldBeOk_Index1()
        {
            var array = new KeyValuePair<string, string>[1];
            _dictionary.CopyTo(array, 0);
            array.Should().BeEquivalentTo(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("key","value")});
        }

        [Fact]
        public void CollectionIsCoherent()
        {
            _dictionary.ShouldBeCoherent();
        }
    }
}
