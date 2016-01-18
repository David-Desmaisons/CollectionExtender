using MoreCollection.Set.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;
using System.Collections;

namespace MoreCollectionTest.Set.Internal
{
    public class ListSetTest
    {
        private ListSet<string> _ListSetSet;
        public ListSetTest()
        {
            _ListSetSet = new ListSet<string>();
            LetterSimpleSetFactory<string>.MaxList = 4;
        }

        [Fact]
        public void New_IsEmpty()
        {
            _ListSetSet.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_WithOtherSizedCollection_ThrowException()
        {
            var collection = new HashSet<string> { "one", "two", "three", "three", "four", "five", "six" };
            Action Do = () => new ListSet<string>(collection);
            Do.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Constructor_WithOneItem_BehaveAsExpected()
        {
            var target = new ListSet<string>("one");
            target.Should().BeEquivalentTo(new[] { "one" });
        }

        [Fact]
        public void Remove_Null_Returnfalse()
        {
            bool res;
            _ListSetSet.Remove(null, out res);
            res.Should().BeFalse();
        }

        [Theory, PropertyData("CollectionData")]
        internal void Add_Returns_SameInstance(ListSet<string> target, string added)
        {
            bool success;
            var res = target.Add(added, out success);

            if (success && (res.Count == LetterSimpleSetFactory<string>.MaxList))
            {
                res.Should().BeOfType<SimpleHashSet<string>>();
            }
            else
            {
                res.Should().BeSameAs(target);
            }
        }

        [Theory, PropertyData("CollectionData")]
        internal void Add_UpdateDicionaryAsExpected(ListSet<string> target, string added)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Add(added);

            var res = target.Add(added, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        [Theory, PropertyData("CollectionData")]
        internal void Remove_ReturnSameInstance(ListSet<string> target, string removed)
        {
            bool success;

            var res = target.Remove(removed, out success);

            res.Should().BeSameAs(target);
        }

        [Theory, PropertyData("OnlyCollectionData")]
        internal void IEnumerable_GetEnumerator_ReturnCorrectCollection(ListSet<string> target)
        {
            System.Collections.IEnumerable nonetype = target;
            var enumerator = nonetype.GetEnumerator();
            var secondenumerator = target.GetEnumerator();
            while (enumerator.MoveNext() && secondenumerator.MoveNext())
            {
                enumerator.Current.Should().Be(secondenumerator.Current);
            }
        }

        [Theory, PropertyData("CollectionData")]
        internal void Remove_UpdateDicionaryAsExpected(ListSet<string> target, string removed)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Remove(removed);

            var res = target.Remove(removed, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        private static ListSet<string> GetEmpty()
        {
            return new ListSet<string>();
        }

        private static ListSet<string> GetBorderLineHashSet()
        {
            return new ListSet<string>(new HashSet<string>() { "one", "two" });
        }

        private static ListSet<string> GetFullHashSet()
        {
            return new ListSet<string>(new HashSet<string>() { "one", "two", "three" });
        }

        public static IEnumerable<object[]> OnlyCollectionData
        {
            get
            {
                yield return new object[] { GetEmpty() };
                yield return new object[] { GetBorderLineHashSet() };
                yield return new object[] { GetFullHashSet() };
            }
        }

        public static IEnumerable<object[]> CollectionData
        {
            get
            {
                yield return new object[] { GetEmpty(), "one" };
                yield return new object[] { GetEmpty(), "five" };
                yield return new object[] { GetEmpty(), "sixteen" };
                yield return new object[] { GetBorderLineHashSet(), "one" };
                yield return new object[] { GetBorderLineHashSet(), "five" };
                yield return new object[] { GetBorderLineHashSet(), "sixteen" };
                yield return new object[] { GetFullHashSet(), "one" };
                yield return new object[] { GetFullHashSet(), "two" };
                yield return new object[] { GetFullHashSet(), "five" };
                yield return new object[] { GetFullHashSet(), "sixteen" };
            }
        }
    }
}
