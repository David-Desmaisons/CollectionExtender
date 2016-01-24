using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;
using MoreCollection.Set.Infra;

namespace MoreCollectionTest.Set.Internal
{
    public class SingleSetTest
    {
        private SingleSet<string> _SingleSetSet;
        public SingleSetTest()
        {
            _SingleSetSet = new SingleSet<string>(null);
        }

        [Fact]
        public void New_IsEmpty()
        {
            _SingleSetSet.Should().BeEmpty();
        }

        [Fact]
        public void Count_IsEmpty_Is0()
        {
            _SingleSetSet.Count.Should().Be(0);
        }

        [Fact]
        public void Count_WithOneElement_Is1()
        {
            var target = new SingleSet<string>(null, "");
            target.Count.Should().Be(1);
        }

        [Theory]
        [InlineData("one")]
        [InlineData("two")]
        [InlineData("three")]
        public void Constructor_WithArgument_ShouldCreateCorrectEnumerable(string value)
        {
            var target = new SingleSet<string>(null, value);
            target.Should().BeEquivalentTo(new object[] { value });
        }

        [Theory, PropertyData("CollectionData")]
        internal void Remove_UpdateDicionaryAsExpected(SingleSet<string> target, string removed)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Remove(removed);

            var res = target.Remove(removed, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        [Theory, PropertyData("CollectionData")]
        internal void Remove_ReturnSelf(SingleSet<string> target, string removed)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Remove(removed);

            var res = target.Remove(removed, out success);

            res.Should().BeSameAs(target);
        }

        [Fact]
        public void Add_Null_Return_False()
        {
            bool res;
            _SingleSetSet.Add(null, out res);
            res.Should().BeFalse();
        }

        [Theory, PropertyData("CollectionData")]
        internal void Add_UpdateDicionaryAsExpected(SingleSet<string> target, string removed)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Add(removed);

            var res = target.Add(removed, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        [Theory, PropertyData("CollectionData")]
        internal void Add_ReturnNewInstanceIfSuccessfull(SingleSet<string> target, string removed)
        {
            bool success;

            var res = target.Add(removed, out success);

            if (res.Count==2)
            {
                res.Should().BeOfType<ListSet<String>>();
            }
            else
            {
                res.Should().BeSameAs(target);
            }
        }

        [Theory, PropertyData("OnlyCollectionData")]
        internal void IEnumerable_GetEnumerator_ReturnCorrectCollection(SingleSet<string> target)
        {
            System.Collections.IEnumerable nonetype = target;
            var enumerator = nonetype.GetEnumerator();
            var secondenumerator = target.GetEnumerator();
            while (enumerator.MoveNext() && secondenumerator.MoveNext())
            {
                enumerator.Current.Should().Be(secondenumerator.Current);
            }
        }

        [Theory]
        [InlineData("one")]
        [InlineData("two")]
        [InlineData("three")]
        [InlineData("four")]
        public void Contains_Should_BeCoherent_ObjectFound(string value)
        {
            var target = new SingleSet<string>(null, value);
            var res = target.Contains(value);
            res.Should().BeTrue();
        }

        [Theory]
        [InlineData("one", "two")]
        [InlineData("two", "one")]
        [InlineData("three", "four")]
        [InlineData("four", "three")]
        public void Contains_Should_BeCoherent_ObjectNotFound(string value, string lookingfor)
        {
            var target = new SingleSet<string>(null, value);
            var res = target.Contains(lookingfor);
            res.Should().BeFalse();
        }

        [Fact]
        public void Contains_Null_IsFalse()
        {
            _SingleSetSet.Contains(null).Should().BeFalse();
        }

        private static ILetterSimpleSetFactory<string> GetFactory()
        {
            return new LetterSimpleSetFactory<string>(4);
        }

        private static SingleSet<string> GetEmpty()
        {
            return new SingleSet<string>(GetFactory());
        }

        private static SingleSet<string> GetFullHashSet()
        {
            return new SingleSet<string>(GetFactory(), "one");
        }

        public static IEnumerable<object[]> OnlyCollectionData
        {
            get
            {
                yield return new object[] { GetEmpty() };
                yield return new object[] { GetFullHashSet() };
            }
        }

        public static IEnumerable<object[]> CollectionData
        {
            get
            {
                yield return new object[] { GetEmpty(), "one" };
                yield return new object[] { GetEmpty(), "two" };
                yield return new object[] { GetFullHashSet(), "one" };
                yield return new object[] { GetFullHashSet(), "two" };
            }
        }
    }
}
