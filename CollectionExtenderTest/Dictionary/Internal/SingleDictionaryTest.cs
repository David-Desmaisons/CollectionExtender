using CollectionExtender.Dictionary.Internal;
using CollectionExtenderTest.TestInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace CollectionExtenderTest.Dictionary.Internal
{
    public class SingleDictionaryTest
    {
        private SingleDictionary<string, string> _dictionary;
        public SingleDictionaryTest()
        {
            _dictionary = new SingleDictionary<string, string>("key","value");
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
        public void Add_ThrowsNotImplementedException()
        {
           Action Do = () => _dictionary.Add("key2","value2");
           Do.ShouldThrow<NotImplementedException>();
        }

        [Fact]
        public void AddKeyValuePair_ThrowsNotImplementedException()
        {
            Action Do = () => _dictionary.Add(new KeyValuePair<string, string> ("key2", "value2"));
            Do.ShouldThrow<NotImplementedException>();
        }

        [Fact]
        public void Remove_ThrowsNotImplementedException()
        {
            Action Do = () => _dictionary.Remove("key");
            Do.ShouldThrow<NotImplementedException>();
        }

        [Fact]
        public void RemoveKeyValuePair_ThrowsNotImplementedException()
        {
            Action Do = () => _dictionary.Remove(new KeyValuePair<string, string>("key2", "value2"));
            Do.ShouldThrow<NotImplementedException>();
        }

        [Fact]
        public void Clear_ThrowsNotImplementedException()
        {
            Action Do = () => _dictionary.Clear();
            Do.ShouldThrow<NotImplementedException>();
        }
        
        [Fact]
        public void IsReadOnly_ThrowsNotImplementedException()
        {
            var res = _dictionary.IsReadOnly;
            res.Should().BeTrue();
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
