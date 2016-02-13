using MoreCollection.Dictionary.Internal.Strategy;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using MoreCollection.Dictionary.Internal;
using MoreCollection.Extensions;
using NSubstitute;

namespace MoreCollectionTest.Dictionary.Internal.Strategy
{
    public abstract class DictionaryStrategyTest<T>
    {
        private DictionaryStrategy<string, string> _DictionaryStrategy;
        internal DictionaryStrategy<string, string> DictionaryStrategy 
        {
            set { _DictionaryStrategy = value; }
        }
      
        private readonly IMutableDictionary<string, string> _MutableDictionary;
        private IDictionary<string, string> _Emulated;
        protected readonly int _Transition = 2;

        public DictionaryStrategyTest()
        {
            _MutableDictionary = Substitute.For<IMutableDictionary<string, string>>();
            _Emulated = new Dictionary<string, string>(){
                {"Key1","Value1"},
                {"Key2","Value2"},
            };    
        }

        private void SetEmulation()
        {
            _MutableDictionary.Count.Returns(_Emulated.Count);
            _MutableDictionary.GetEnumerator().Returns(args => _Emulated.GetEnumerator());
            _MutableDictionary.When(md => md.CopyTo(Arg.Any<KeyValuePair<string, string>[]>(), Arg.Any<int>()))
                            .Do((arg => _Emulated.CopyTo((KeyValuePair<string, string>[])(arg[0]), (int)arg[1])));
            _MutableDictionary.ContainsKey(Arg.Any<string>()).Returns(arg => _Emulated.ContainsKey((string)arg[0]));
        }

        private void ChechHasTransitioned<TTest>(IMutableDictionary<string, string> res)
        {
            _MutableDictionary.Should().NotBeSameAs(res);
            res.Should().BeOfType<TTest>();
        }

        private void ChechHasTransitioned_Intermediate(IMutableDictionary<string, string> res)
        {
            ChechHasTransitioned<T>(res);
        }

        private void ChechHasTransitioned_Dictionary(IMutableDictionary<string, string> res)
        {
            ChechHasTransitioned<MutableDictionary<string, string>>(res);
        }

        private void ChechHasTransitioned_Single(IMutableDictionary<string, string> res)
        {
            ChechHasTransitioned<MutableSingleDictionary<string, string>>(res);
        }

        [Fact]
        public void GetEmpty_Return_Empty_Collection()
        {
            var res = _DictionaryStrategy.GetEmpty();
            res.Should().BeEmpty();
        }

        [Fact]
        public void GetEmpty_Return_MutableSingleDictionary()
        {
            var res = _DictionaryStrategy.GetEmpty();
            res.Should().BeOfType<MutableSingleDictionary<string, string>>();
        }

        [Fact]
        public void GetEmpty_CalledWith1_Return_MutableSingleDictionary()
        {
            var res = _DictionaryStrategy.GetEmpty(1);
            res.Should().BeEmpty();
            res.Should().BeOfType<MutableSingleDictionary<string, string>>();
        }

        [Fact]
        public void GetEmpty_CalledWith_Transition_Return_IntermediateDictionary()
        {
            var res = _DictionaryStrategy.GetEmpty(_Transition);
            res.Should().BeEmpty();
            res.Should().BeOfType<T>();
        }

        [Fact]
        public void GetEmpty_CalledWith_TransitionPlusOne_Return_MutableListDictionary()
        {
            var res = _DictionaryStrategy.GetEmpty(_Transition+1);
            res.Should().BeEmpty();
            res.Should().BeOfType<MutableDictionary<string, string>>();
        }

        [Fact]
        public void GetIntermediateCollection_Return_MutableDictionary_OfCorrectType()
        {
            var res = _DictionaryStrategy.GetIntermediateCollection(_MutableDictionary);
            res.Should().BeOfType<T>();
        }

        [Fact]
        public void GetIntermediateCollection_Return_MutableDictionary_WithCorrectInitialization()
        {
            SetEmulation();
            var res = _DictionaryStrategy.GetIntermediateCollection(_MutableDictionary);
            res.AsEnumerable().Should().BeEquivalentTo(_Emulated);
        }

        [Fact]
        public void CheckDictionaryRemoved_Return_SameInstance_WhenCountIsMoreThanTransition()
        {
            var res = SetUpRemoveCheckDictionaryRemoved(_Transition + 1);
            _MutableDictionary.Should().BeSameAs(res);
        }

        [Fact]
        public void CheckDictionaryRemoved_Return_IntermediateMutableCollection_WhenCountIsEqualTransition()
        {
            var res = SetUpRemoveCheckDictionaryRemoved(_Transition);
            ChechHasTransitioned_Intermediate(res);
        }

        private  IMutableDictionary<string, string> SetUpRemoveCheckDictionaryRemoved(int transition)
        {
            var dum = _MutableDictionary.Count.Returns(transition);
            return _DictionaryStrategy.CheckDictionaryRemoved(_MutableDictionary);
        }

        [Fact]
        public void Remove_Return_SameInstance_WhenCountIsMoreOne()
        {
            var res = SetUpRemove(2);
            _MutableDictionary.Should().BeSameAs(res);
        }

        [Fact]
        public void Remove_Return_SingleDictionaryInstance_WhenCountIsOne()
        {
            var res = SetUpRemove(1);
            ChechHasTransitioned_Single(res);
        }

        [Fact]
        public void Remove_CallMutableCollectionRemove()
        {
            var key = "key";
            SetUpRemove(2, key);
            _MutableDictionary.Received(1).Remove(key);
        }

        private IMutableDictionary<string, string>  SetUpRemove(int collectionValue, string key="")
        {
            _Emulated = new Dictionary<string, string>();
            Enumerable.Range(0, collectionValue)
                .ForEach(i => _Emulated.Add(string.Format("Key{0}", i), string.Format("Value{0}", i)));
            SetEmulation();
            bool result;
            return _DictionaryStrategy.Remove(_MutableDictionary, key, out result);
        }

        [Fact]
        public void Update_Return_SameInstance_IfCountBelowTransition()
        {
            var res = SetUpdate(_Transition-1);
            _MutableDictionary.Should().BeSameAs(res);
        }

        [Fact]
        public void Update_Return_SameInstance_IfCountIsTransitionButContainsKey()
        {
            var res = SetUpdate(_Transition, "Key0");
            _MutableDictionary.Should().BeSameAs(res); 
        }

        [Fact]
        public void Update_Return_NewInstance_IfCountIsTransitionButContainsKey()
        {
            var res = SetUpdate(_Transition, "Key4");
            ChechHasTransitioned_Dictionary(res);
        }

        [Fact]
        public void Update_Return_NewInstanceWithCorrectInformation_IfCountIsTransitionButContainsKey()
        {
            var res = SetUpdate(_Transition, "Key2", "Value2");
            _Emulated.Add("Key2", "Value2");
            res.AsEnumerable().Should().BeEquivalentTo(_Emulated);
        }

        [Fact]
        public void Update_CallItemsIndex_WhenMutableIsNotUpdated()
        {
            var res = SetUpdate(_Transition, "Key0", "V2");
            _MutableDictionary.Received(1)["Key0"] = "V2";
        }

        private IMutableDictionary<string, string> SetUpdate(int collectionValue, string key="key", string value="value")
        {
            _Emulated = new Dictionary<string, string>();
            Enumerable.Range(0, collectionValue)
                .ForEach(i => _Emulated.Add(string.Format("Key{0}", i), string.Format("Value{0}", i)));
            SetEmulation();
            return _DictionaryStrategy.Update(_MutableDictionary, key, value);
        }

        [Fact]
        public void Add_Return_SameInstance_IfCountLessThanTransition()
        {
            var res = SetAdd(_Transition-1, "Key4", "V2");
            _MutableDictionary.Should().BeSameAs(res);
        }

        [Fact]
        public void Add_Return_DictionaryInstance_IfCountTransition()
        {
            var res = SetAdd(_Transition, "Key4", "V2");
            ChechHasTransitioned_Dictionary(res);
        }

        [Fact]
        public void Add_Return_DictionaryInstanceWithCorrectInformation_IfCountTransition()
        {
            var res = SetAdd(_Transition, "Key2", "Value2");
            _Emulated.Add("Key2", "Value2");
            res.AsEnumerable().Should().BeEquivalentTo(_Emulated);
        }

        [Fact]
        public void Add_MutableDictionaryAdd_WhenMutableIsNotUpdated()
        {
            var res = SetAdd(_Transition-1, "Key4", "V2");
            _MutableDictionary.Received(1).Add("Key4","V2");
        }

        private IMutableDictionary<string, string> SetAdd(int collectionValue, string key = "key", string value = "value")
        {
            _Emulated = new Dictionary<string, string>();
            Enumerable.Range(0, collectionValue)
                .ForEach(i => _Emulated.Add(string.Format("Key{0}", i), string.Format("Value{0}", i)));
            SetEmulation();
            return _DictionaryStrategy.Add(_MutableDictionary, key, value);
        }
    }
}
