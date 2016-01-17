using CollectionExtender.Set.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace CollectionExtenderTest.Set.Internal
{
    public class SimpleHashSetTest : IDisposable
    {
        private SimpleHashSet<string> _SimpleHashSet;
        public SimpleHashSetTest()
        {
            _SimpleHashSet = new SimpleHashSet<string>();
            LetterSimpleSetFactory<string>.MaxList = 4;
        }

        public void Dispose()
        {
            LetterSimpleSetFactory<string>.MaxList = 10;
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
            var target = new SimpleHashSet<string>(collection);
            target.Should().BeEquivalentTo(expected);
        }

        [Theory, PropertyData("CollectionData")]
        internal void Add_Returns_SameInstance(SimpleHashSet<string> target, string added)
        {
            bool success;
            var res = target.Add(added, out success);

            res.Should().BeSameAs(target);
        }

        [Theory, PropertyData("CollectionData")]
        internal void Add_UpdateDicionaryAsExpected(SimpleHashSet<string> target, string added)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Add(added);

            var res = target.Add(added, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        [Theory, PropertyData("CollectionData")]
        internal void Remove_ReturnDifferentInstance_IfLimitReaches(SimpleHashSet<string> target, string removed)
        {
            bool success;

            var res = target.Remove(removed, out success);

            if (res.Count == LetterSimpleSetFactory<string>.MaxList-1)
            {
                res.Should().BeOfType<ListSet<string>>();
            }
            else
            {
                res.Should().BeSameAs(target);
            }
        }

        [Theory, PropertyData("CollectionData")]
        internal void Remove_UpdateDicionaryAsExpected(SimpleHashSet<string> target, string removed)
        {
            bool success;
            var expected = new HashSet<string>(target);
            bool result = expected.Remove(removed);

            var res = target.Remove(removed, out success);

            res.Should().BeEquivalentTo(expected);
            result.Should().Be(success);
        }

        public static IEnumerable<object[]> CollectionData
        {
            get
            {
                var EmptyHashSet = new SimpleHashSet<string>();
                var BorderLineHashSet = new SimpleHashSet<string>(new[] { "one", "two", "three", "four" });
                var FullHashSet = new SimpleHashSet<string>(new[] { "one", "two", "three", "four", "five" });

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
