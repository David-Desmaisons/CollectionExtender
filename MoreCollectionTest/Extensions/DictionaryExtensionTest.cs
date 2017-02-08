using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using NSubstitute;
using MoreCollection.Extensions;

namespace MoreCollectionTest.Extensions
{
    public class DictionaryExtensionTest
    {
        private readonly Dictionary<string, string> _Dictionary;
        private readonly Dictionary<string, string> _NullDictionary=null;
        private readonly Func<string, string> _Creator;
        private readonly Func<string, string, string> _Updater;
        private readonly Action<string, string> _Updater2;

        public DictionaryExtensionTest()
        {
            _Dictionary = new Dictionary<string, string>();
            _Creator = Substitute.For<Func<string, string>>();
            _Updater = Substitute.For<Func<string, string, string>>();
            _Updater2 = Substitute.For<Action<string, string>>();
        }

        [Fact]
        public void GetOrAdd_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.GetOrAdd("Key", _ => "value");
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetOrAdd_CreateEntity_IfNotPresent()
        {
            var res = _Dictionary.GetOrAdd("Key", _ => "value");
            res.Item.Should().Be("value");
            res.CollectionStatus.Should().Be(CollectionStatus.Created);
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void GetOrAdd_FindEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.GetOrAdd("Key", _ => "value2");
            res.Item.Should().Be("value");
            res.CollectionStatus.Should().Be(CollectionStatus.Found);
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void GetOrAdd_DoNotCallFunction_IfItemFound()
        {
            _Dictionary.Add("Key", "value");
            Func<string, string> func = Substitute.For<Func<string, string>>();
            _Dictionary.GetOrAdd("Key", func);
            func.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void GetOrAdd_CallFunction_WithKeyValue()
        {
            _Dictionary.GetOrAdd("Key", _Creator);
            _Creator.Received(1).Invoke("Key");
        }

        [Fact]
        public void GetOrAddEntity_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.GetOrAddEntity("Key", _ => "value");
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetOrAddEntity_CreateEntity_IfNotPresent()
        {
            var res = _Dictionary.GetOrAddEntity("Key", _ => "value");
            res.Should().Be("value");
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void GetOrAddEntity_FindEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.GetOrAddEntity("Key", _ => "value2");
            res.Should().Be("value");
            _Dictionary.AsEnumerable().Should().BeEquivalentTo(new[] { new KeyValuePair<string, string>("Key", "value") });
        }

        [Fact]
        public void GetOrAddEntity_DoNotCallFunction_IfItemFound()
        {
            _Dictionary.Add("Key", "value");
            _Dictionary.GetOrAddEntity("Key", _Creator);
            _Creator.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void GetOrAddEntity_CallFunction_WithKeyValue()
        {
            _Dictionary.GetOrAddEntity("Key", _Creator);
            _Creator.Received(1).Invoke("Key");
        }

        [Fact]
        public void UpdateOrAdd_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.UpdateOrAdd("Key", _ => "value", (k,v) => v);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void UpdateOrAdd_AddEntity_IfNotPresent()
        {
            var res = _Dictionary.UpdateOrAdd("Key", _ => "value", _Updater);
            res.Should().Be("value");
            _Updater.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void UpdateOrAdd_UpdateEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.UpdateOrAdd("Key", _Creator , (k,v) => "value2");
            res.Should().Be("value2");
            _Creator.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void UpdateOrAdd2_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.UpdateOrAdd("Key", _ => "value", _Updater2);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void UpdateOrAdd2_AddEntity_IfNotPresent()
        {
            var res = _Dictionary.UpdateOrAdd("Key", _ => "value", _Updater2);
            res.Should().Be("value");
            _Updater2.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void UpdateOrAdd2_UpdateEntity_IfPresent()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.UpdateOrAdd("Key", _Creator, _Updater2);
            res.Should().Be("value");
            _Updater2.Received(1).Invoke(Arg.Any<string>(), Arg.Any<string>());
            _Creator.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Fact]
        public void GetOrDefault_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullDictionary.GetOrDefault("Key");
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetOrDefault_ReturnValue_WhenFound()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.GetOrDefault("Key");
            res.Should().Be("value");
        }

        [Fact]
        public void GetOrDefault_ReturnDefault_WhenNotFound()
        {
            var res = _Dictionary.GetOrDefault("Key");
            res.Should().BeNull();
        }

        [Fact]
        public void GetOrDefaultWithValue_ReturnValue_WhenFound()
        {
            _Dictionary.Add("Key", "value");
            var res = _Dictionary.GetOrDefault("Key", "not found");
            res.Should().Be("value");
        }

        [Fact]
        public void GetOrDefaultWithValue_ReturnDefault_WhenNotFound()
        {
            var res = _Dictionary.GetOrDefault("Key", "not found");
            res.Should().Be("not found");
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
