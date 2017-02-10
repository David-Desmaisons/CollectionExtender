using MoreCollection.Set.Infra;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace MoreCollectionTest.Set.Internal
{
    public class SimpleHashSetTest 
    {
        private SimpleHashSet<string> _SimpleHashSet;
        private static int _Transition = 4;
        public SimpleHashSetTest()
        {
            _SimpleHashSet = new SimpleHashSet<string>(null);
        }

        [Fact]
        public void LetterSimpleSetFactory_Inherit_FromHashSet()
        {
            _SimpleHashSet.Should().BeAssignableTo<HashSet<string>>();
        }

        [Fact]
        public void New_IsEmpty()
        {
            _SimpleHashSet.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_WithEnumerator_RemovesDuplicate()
        {
            var collection = new[] { "one", "two", "three", "three", "four" };
            var expected = new[] { "one", "two", "three", "four" };
            var target = new SimpleHashSet<string>(null, collection);
            target.Should().BeEquivalentTo(expected);
        }

        [Theory, MemberData("CollectionData")]
        internal void Add_Returns_SameInstance(SimpleHashSet<string> target, string added)
        {
            bool success;
            var res = target.Add(added, out success);

            res.Should().BeSameAs(target);
        }

        [Theory, MemberData("CollectionData")]
        internal void Add_UpdateDicionaryAsExpected(SimpleHashSet<string> target, string added)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Add(added);

            var res = target.Add(added, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        [Theory, MemberData("CollectionData")]
        internal void Remove_ReturnDifferentInstance_IfLimitReaches(SimpleHashSet<string> target, string removed)
        {
            bool success;

            var res = target.Remove(removed, out success);

            if (res.Count == _Transition-1)
            {
                res.Should().BeOfType<ListSet<string>>();
            }
            else
            {
                res.Should().BeSameAs(target);
            }
        }

        [Theory, MemberData("CollectionData")]
        internal void Remove_UpdateDicionaryAsExpected(SimpleHashSet<string> target, string removed)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Remove(removed);

            var res = target.Remove(removed, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        private static ILetterSimpleSetFactory GetFactory()
        {
            return new LetterSimpleSetFactory(_Transition);
        }

        public static IEnumerable<object[]> CollectionData
        {
            get
            {
                var EmptyHashSet = new SimpleHashSet<string>(GetFactory());
                var BorderLineHashSet = new SimpleHashSet<string>(GetFactory(), new[] { "one", "two", "three", "four" });
                var FullHashSet = new SimpleHashSet<string>(GetFactory(), new[] { "one", "two", "three", "four", "five" });

                yield return new object[] { EmptyHashSet, "one" };
                yield return new object[] { EmptyHashSet, "five" };
                yield return new object[] { EmptyHashSet, "sixteen" };
                yield return new object[] { BorderLineHashSet, "one" };
                yield return new object[] { BorderLineHashSet, "five" };
                yield return new object[] { BorderLineHashSet, "sixteen" };
                yield return new object[] { FullHashSet, "one" };
                yield return new object[] { FullHashSet, "two" };
                yield return new object[] { FullHashSet, "five" };
                yield return new object[] { FullHashSet, "sixteen" };
            }
        }
    }
}
