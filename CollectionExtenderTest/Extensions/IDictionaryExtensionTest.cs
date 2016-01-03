using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NSubstitute;
using CollectionExtender.Extensions;

namespace CollectionExtenderTest.Extensions
{
    public class IDictionaryExtensionTest
    {
        private Dictionary<string, string> _Dictionary;
        private Dictionary<string, string> _NullDictionary;
        private Func<string, string> _Creator;
        private Func<string, string, string> _Updater;
        private Action<string, string> _Updater2;

        public IDictionaryExtensionTest()
        {
            _Dictionary = new Dictionary<string, string>();
            _Creator = Substitute.For<Func<string, string>>();
            _Updater = Substitute.For<Func<string, string, string>>();
            _Updater2 = Substitute.For<Action<string, string>>();
        }

        [Fact]
        public void FindOrCreate_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.FindOrCreate("Key", _ => "value");
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void FindOrCreate_CreateEntity_IfNotPresent()
        {
            var res = _Dictionary.FindOrCreate("Key", _ => "value");
            res.Item.Should().Be("value");
            res.CollectionStatus.Should().Be(CollectionStatus.Created);
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void FindOrCreate_FindEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.FindOrCreate("Key", _ => "value2");
            res.Item.Should().Be("value");
            res.CollectionStatus.Should().Be(CollectionStatus.Found);
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void FindOrCreate_DoNotCallFunction_IfItemFound()
        {
            _Dictionary.Add("Key", "value");
            Func<string, string> func = Substitute.For<Func<string, string>>();
            _Dictionary.FindOrCreate("Key", func);
            func.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void FindOrCreate_CallFunction_WithKeyValue()
        {
            _Dictionary.FindOrCreate("Key", _Creator);
            _Creator.Received(1).Invoke("Key");
        }

        [Fact]
        public void FindOrCreateEntity_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.FindOrCreateEntity("Key", _ => "value");
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void FindOrCreateEntity_CreateEntity_IfNotPresent()
        {
            var res = _Dictionary.FindOrCreateEntity("Key", _ => "value");
            res.Should().Be("value");
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void FindOrCreateEntity_FindEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.FindOrCreateEntity("Key", _ => "value2");
            res.Should().Be("value");
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void FindOrCreateEntity_DoNotCallFunction_IfItemFound()
        {
            _Dictionary.Add("Key", "value");
            _Dictionary.FindOrCreateEntity("Key", _Creator);
            _Creator.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void FindOrCreateEntity_CallFunction_WithKeyValue()
        {
            _Dictionary.FindOrCreateEntity("Key", _Creator);
            _Creator.Received(1).Invoke("Key");
        }

        [Fact]
        public void UpdateOrCreate_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.UpdateOrCreate("Key", _ => "value", (k,v) => v);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void UpdateOrCreate_AddEntity_IfNotPresent()
        {
            var res = _Dictionary.UpdateOrCreate("Key", _ => "value", _Updater);
            res.Should().Be("value");
            _Updater.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void UpdateOrCreate_UpdateEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.UpdateOrCreate("Key", _Creator , (k,v) => "value2");
            res.Should().Be("value2");
            _Creator.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void UpdateOrCreate2_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.UpdateOrCreate("Key", _ => "value", _Updater2);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void UpdateOrCreate2_AddEntity_IfNotPresent()
        {
            var res = _Dictionary.UpdateOrCreate("Key", _ => "value", _Updater2);
            res.Should().Be("value");
            _Updater2.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void UpdateOrCreate2_UpdateEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.UpdateOrCreate("Key", _Creator, _Updater2);
            res.Should().Be("value");
            _Updater2.Received(1).Invoke(Arg.Any<string>(), Arg.Any<string>());
            _Creator.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void Import_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.Import( new Dictionary<string,string>());
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Import_CalledWithNullArgument_ThrowException()
        {
            Action Do = () => _Dictionary.Import(_NullDictionary);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Import_Append_Dictionary()
        {
            _Dictionary.Add("Key", "value");
            var dictionary2 = new Dictionary<string,string>(){{"Key2","value2"}};
            _Dictionary.Import(dictionary2);
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { 
                            new KeyValuePair<string, string>("Key", "value"),
                            new KeyValuePair<string, string>("Key2", "value2")
            });
        }

        [Fact]
        public void Import_return_CallingArgument()
        {
            _Dictionary.Add("Key", "value");
            var dictionary2 = new Dictionary<string, string>() { { "Key2", "value2" } };
            var res = _Dictionary.Import(dictionary2);
            res.Should().Equal(_Dictionary);
        }
    }
}
