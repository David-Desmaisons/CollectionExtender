using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using MoreCollection.Extensions;

namespace MoreCollectionTest.Set
{
    public abstract class SetTest
    {
        private readonly ISet<string> _Set;
        private readonly ISet<string> _ReferenceSet;

        protected SetTest(ISet<string> set)
        {
            _Set = set;
            _ReferenceSet = new HashSet<string>();
        }

        private void SetUp(Action< ISet<string>> Do)
        {
            Do(_Set);
            Do(_ReferenceSet);
        }

        private void Test(Action<ISet<string>> Do)
        {
            Do(_Set);
            Do(_ReferenceSet);
            _Set.Should().BeEquivalentTo(_ReferenceSet);
        }

        private void Test<T>(Func<ISet<string>, T> Do)
        {
            var res = Do(_Set);
            var res2 = Do(_ReferenceSet);
            res.Should().Be(res2);
            _Set.Should().BeEquivalentTo(_ReferenceSet);
        }

        [Fact]
        public void DefaultConstructor_ReturnEmpty()
        {
            _Set.Should().BeEmpty();
        }

        [Fact]
        public void Add_ShouldAddElement()
        {
            Test( s => s.Add("aa"));
        }

        [Fact]
        public void Add_ShouldNotAddElement_IfAlreadyPresent()
        {
            SetUp(s => s.Add("aa"));
            Test(s => s.Add("aa"));
        }

        [Theory, MemberData(nameof(Data))]
        public void StressTestAdd(string[] strings)
        {
            SetUp(s => strings.ForEach(r => s.Add(r)));
            Test(s => s.Add("aa"));
        }

        [Theory, MemberData(nameof(Data))]
        public void StressTestAdd_2(string[] strings)
        {
            Test(s => { strings.ForEach(r => s.Add(r)); });
        }

        public static IEnumerable<string[]> RawData
        {
            get
            {
                yield return new[] { "a", "b" };
                yield return new[] { "a", "b", "c" };
                yield return new[] { "a", "b", "c", "d" };
                yield return new[] { "a", "b", "c", "a" };
            }
        }

        [Fact]
        public void Remove_ShouldRemoveElement()
        {
            SetUp(s => s.Add("aa"));
            Test(s => s.Remove("aa"));
        }

        [Fact]
        public void Remove_ShouldNotRemoveElement_IfNotPresent()
        {
            Test(s => s.Remove("aa"));
        }

        [Theory, MemberData(nameof(Data))]
        public void StressTestRemove(string[] strings)
        {
            SetUp(s => strings.ForEach(r => s.Add(r)));
            Test(s => s.Remove("c"));
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                return RawData.Select(collection => new[] { collection });
            }
        }
    }
}
