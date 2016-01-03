using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CollectionExtenderTest.TestInfra;
using Xunit.Extensions;

namespace CollectionExtenderTest.TestInfra
{
    public abstract class DictionaryTest
    {
        protected IDictionary<string, string> _dictionary;
        private IDictionary<string, string> _target;
        private List<string> _Obj;

        public DictionaryTest()
        {
            _target = new Dictionary<string, string>();
            _Obj = Enumerable.Range(0,30).Select(i => string.Format("Name{0}", i)).ToList();
        }

        private void Do(Action<IDictionary<string, string>> Act)
        {
            Act(_target);
            Act(_dictionary);
        }

        [Fact]
        public void IsReadOnly_IsFalse()
        {
            _dictionary.IsReadOnly.Should().BeFalse();
        }

        [Fact]
        public void Add_WorkAsExpected_Adding_EmptyDictionary()
        {
            _dictionary.ShouldBehaveTheSame(_target, d => d.Add("toto", "Value0"));
        }

        [Fact]
        public void Add__Adding_PopulatedCollection()
        {
            Do(d => d.Add("toto", "Value0"));
            _dictionary.ShouldBehaveTheSame(_target, d => d.Add("toto2", "Value0"));
        }

        [Fact]
        public void Add_DuplicatedKey_ThrowsException()
        {
            Do ( d => d.Add("toto", "Value0"));
            _dictionary.ShouldBehaveTheSame(_target, d => d.Add("toto", "Value0"),typeof(ArgumentException));
        }

        [Fact]
        public void Add_NullKey_ThrowsException()
        {
            _dictionary.ShouldBehaveTheSame(_target, d => d.Add(null, "Value0"),typeof(ArgumentNullException));
        }

        [Theory]
        [InlineData("K1", "V1")]
        [InlineData("K2", "V2")]
        [InlineData("Key", "Value")]
        [InlineData("K0", "V0")]
        public void AddKeyValue_Set_NewKey_InsertKey(string key, string Value)
        {
            _dictionary.ShouldBehaveTheSame(_target, d => d.Add(new KeyValuePair<string, string>(key, Value)));
            _dictionary[key].Should().Be(Value);
        }

        [Theory]
        [InlineData("K1", "V1", "V2")]
        [InlineData("K2", "V2", "newvalue")]
        [InlineData("Key", "Value", "newvalue2")]
        [InlineData("K0", "V0", "J0")]
        public void AddKeyValue_Set_ExistingKey_ThrowException(string key, string Value, string Value2)
        {
            Do(d => d.Add(key, Value));
            _dictionary.ShouldBehaveTheSame(_target, d => d.Add(new KeyValuePair<string, string>(key, Value2)), typeof(ArgumentException));
        }

        [Fact]
        public void Remove_NullKey_ThrowsException()
        {
            _dictionary.ShouldBehaveTheSame(_target, d => d.Remove(null), typeof(ArgumentNullException));
        }

        [Fact]
        public void Remove_NoneExistingKey_ReturnFalse()
        {
            var ok = _dictionary.ShouldBehaveTheSame(_target, d => d.Remove("ko"));
            ok.Should().BeFalse();
        }

        [Fact]
        public void Remove_ExistingKey_ReturnTrue()
        {
            Do(d => d.Add("toto", "Value0"));
            var ok = _dictionary.ShouldBehaveTheSame(_target, d => d.Remove("toto"));
            ok.Should().BeTrue();
            _dictionary.AsEnumerable().Should().BeEmpty();
        }

        [Theory]
        [InlineData("K1", "V1")]
        [InlineData("K2", "V2")]
        [InlineData("Key", "Value")]
        [InlineData("K0", "V0")]
        public void RemoveKeyValuePair_NoneExistingKey_ReturnFalse(string key, string value)
        {
            var res = _dictionary.ShouldBehaveTheSame(_target, d => d.Remove(new KeyValuePair<string, string>(key, value)));
            res.Should().BeFalse();
        }


        [Theory]
        [InlineData("K1", "V1")]
        [InlineData("K2", "V2")]
        [InlineData("Key", "Value")]
        [InlineData("K0", "V0")]
        public void RemoveKeyValuePair_NoneExistingKey_NoneEmptyDictionary_ReturnFalse(string key, string value)
        {
            Do(d => d.Add("toto", "Value0"));
            var res = _dictionary.ShouldBehaveTheSame(_target, d => d.Remove(new KeyValuePair<string, string>(key, value)));
            res.Should().BeFalse();
        }

        [Theory]
        [InlineData("t", "V1")]
        [InlineData("K2", "V2")]
        [InlineData("Key", "Value")]
        [InlineData("K0", "V0")]
        public void RemoveKeyValuePair_NoneExistingValue_ReturnFalse(string key, string value)
        {
            Do(d => d.Add(key, value));
            var res = _dictionary.ShouldBehaveTheSame(_target, d => d.Remove(new KeyValuePair<string, string>(key, value+"8")));
            res.Should().BeFalse();
        }


        [Theory]
        [InlineData("t", "V1")]
        [InlineData("K2", "V2")]
        [InlineData("Key", "Value")]
        [InlineData("K0", "V0")]
        public void RemoveKeyValuePair_ExistingKeyValue_ReturnFalse(string key, string value)
        {
            Do(d => d.Add(key, value));
            var res = _dictionary.ShouldBehaveTheSame(_target, d => d.Remove(new KeyValuePair<string, string>(key, value)));
            res.Should().BeTrue();
            _dictionary.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void TryGetValue_NoneExistingKey_ReturnFalse()
        {
            string res;
            var ok = _dictionary.TryGetValue("toto", out res);
            ok.Should().BeFalse();
            res.Should().BeNull();
        }

        [Fact]
        public void TryGetValue_ExistingKey_ReturnTrue()
        {
            _dictionary.Add("toto", "Value0");
            string res;
            var ok = _dictionary.TryGetValue("toto", out res);
            ok.Should().BeTrue();
            res.Should().Be("Value0");
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
            _dictionary.Add("toto", "Value0");
            var ok = _dictionary.ContainsKey("toto");
            ok.Should().BeTrue();
        }

        [Fact]
        public void Indexer_Get_NoneExistingKey_ThrowKeyNotFoundException()
        {
            string value;
            _dictionary.ShouldBehaveTheSame(_target, d => value = d["Key0"], typeof(KeyNotFoundException));
        }

        [Theory]
        [InlineData("K1","V1")]
        [InlineData("K2", "V2")]
        [InlineData("Key", "Value")]
        [InlineData("K0", "V0")]
        public void Indexer_Get_ExistingKey_ReturnExpectedValue(string key, string Value)
        {
            Do(d => d.Add(key, Value));
            string value = _dictionary.ShouldBehaveTheSame(_target, d => value = d[key]);
            value.Should().Be(Value);
        }

        [Theory]
        [InlineData("K1", "V1")]
        [InlineData("K2", "V2")]
        [InlineData("Key", "Value")]
        [InlineData("K0", "V0")]
        public void Indexer_Set_NoneExistingKey_AddCorrespondingEntry(string key, string Value)
        {
            _dictionary.ShouldBehaveTheSame(_target, d => d[key] = Value);
            _dictionary[key].Should().Be(Value);
        }

        [Theory]
        [InlineData("K1", "V1", "V2")]
        [InlineData("K2", "V2", "newvalue")]
        [InlineData("Key", "Value", "newvalue2")]
        [InlineData("K0", "V0", "J0")]
        public void Indexer_Set_ExistingKey_UpdateCorrespondingEntry(string key, string Value, string Value2)
        {
            Do(d => d.Add(key, Value));
            _dictionary.ShouldBehaveTheSame(_target, d => d[key] = Value2);
            _dictionary[key].Should().Be(Value2);
        }

        [Fact]
        public void Clear_EmptyCollection()
        {
            Do(d => { d.Add("k1", "v1"); d.Add("k2", "v2"); } );
            _dictionary.ShouldBehaveTheSame(_target, d => d.Clear());
            _dictionary.AsEnumerable().Should().BeEmpty();
        }

        [Fact]
        public void CopyTo_Null_ThrowException()
        {
            Action Do = () => _dictionary.CopyTo(null, 0);
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
        public void CopyTo_ShouldBeOk_Index1()
        {
            _dictionary.Add("key", "value");
            var array = new KeyValuePair<string, string>[1];
            _dictionary.CopyTo(array, 0);
            array.Should().BeEquivalentTo(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("key","value")});
        }

        [Fact]
        public void CopyTo_ShouldBeOk_EmptyCollection()
        {
            var array = new KeyValuePair<string, string>[1];
            _dictionary.CopyTo(array, 0);
        }
    }
}
