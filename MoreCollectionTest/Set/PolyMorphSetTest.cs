using MoreCollection.Set;
using MoreCollection.Set.Infra;
using MoreCollection.Extensions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace MoreCollectionTest.Set
{
    public class PolyMorphSetTest : IDisposable
    {
        private readonly ILetterSimpleSetFactory<string> _LetterSimpleSetFactory;
        private readonly ILetterSimpleSet<string> _LetterSimpleSetSubstitute;
        private readonly IEnumerable<string> _Enumerable;

        public PolyMorphSetTest()
        {
            _Enumerable = Substitute.For<IEnumerable<string>>();
            _LetterSimpleSetSubstitute = Substitute.For<ILetterSimpleSet<string>>();
            _LetterSimpleSetFactory = Substitute.For<ILetterSimpleSetFactory<string>>();
            _LetterSimpleSetFactory.GetDefault().Returns(_LetterSimpleSetSubstitute);
            _LetterSimpleSetFactory.GetDefault(Arg.Any<string>()).Returns(_LetterSimpleSetSubstitute);
            _LetterSimpleSetFactory.GetDefault(Arg.Any<IEnumerable<string>>()).Returns(_LetterSimpleSetSubstitute);
            bool res;
            _LetterSimpleSetSubstitute.Add(Arg.Any<string>(), out res).Returns(_LetterSimpleSetSubstitute);
            LetterSimpleSetFactory<string>.Factory = _LetterSimpleSetFactory;
        }

        private void SetUp(HashSet<string> FakeCollection)
        {
            _LetterSimpleSetSubstitute.GetEnumerator().Returns(FakeCollection.GetEnumerator());
            _LetterSimpleSetSubstitute.Contains(Arg.Any<string>())
                                .Returns(argument => FakeCollection.Contains(argument[0]));
        }

        [Fact]
        public void Constructor_WithoutParameters_Call_LetterSimpleSetFactory_GetDefault_WithoutParameters()
        {
            var PolyMorphSet = new PolyMorphSet<string>();
            _LetterSimpleSetFactory.Received(1).GetDefault();
        }

        [Fact]
        public void Constructor_WithoneElement_Call_LetterSimpleSetFactory_GetDefault_WithoutoneElement()
        {
            var PolyMorphSet = new PolyMorphSet<string>("abcd");
            _LetterSimpleSetFactory.Received(1).GetDefault("abcd");
        }

        [Fact]
        public void Constructor_WithEnumerable_Call_LetterSimpleSetFactory_GetDefault_WithoutEnumerable()
        {
            var PolyMorphSet = new PolyMorphSet<string>(_Enumerable);
            _LetterSimpleSetFactory.Received(1).GetDefault(_Enumerable);
        }

        [Fact]
        public void Add_Call_LetterSimpleSet_Add()
        {
            bool res;
            var PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.Add("key");
            _LetterSimpleSetSubstitute.Received(1).Add("key", out res);
        }

         [Fact]
        public void Add_ICollection_Call_LetterSimpleSet_Add()
        {
            bool res;
            ICollection<string> PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.Add("key");
            _LetterSimpleSetSubstitute.Received(1).Add("key", out res);
        }

        [Fact]
        public void Remove_Call_LetterSimpleSet_Remove()
        {
            bool res;
            var PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.Remove("key");
            _LetterSimpleSetSubstitute.Received(1).Remove("key", out res);
        }

        [Fact]
        public void Contains_Call_LetterSimpleSet_Contains()
        {
            var PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.Contains("key");
            _LetterSimpleSetSubstitute.Received(1).Contains("key");
        }

        [Fact]
        public void Count_Call_LetterSimpleSet_Count()
        {
            var PolyMorphSet = new PolyMorphSet<string>();
            var count = PolyMorphSet.Count;
            var res = _LetterSimpleSetSubstitute.Received(1).Count;
        }

         [Fact]
        public void Clear_Call_LetterSimpleSetFactory_GetDefault()
        {
            var PolyMorphSet = new PolyMorphSet<string>();
            _LetterSimpleSetFactory.ClearReceivedCalls();
            PolyMorphSet.Clear();
            _LetterSimpleSetFactory.Received(1).GetDefault();
        }
        

        [Fact]
        public void GetEnumerator_Call_LetterSimpleSet_GetEnumerator()
        {
            var PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.GetEnumerator();
            _LetterSimpleSetSubstitute.Received(1).GetEnumerator();
        }

        [Fact]
        public void Collections_GetEnumerator_Call_LetterSimpleSet_GetEnumerator()
        {
            System.Collections.IEnumerable PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.GetEnumerator();
            _LetterSimpleSetSubstitute.Received(1).GetEnumerator();
        }

        [Fact]
        public void IsReadOnly_IsFalse()
        {
            var PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.IsReadOnly.Should().BeFalse();
        }

        public static IEnumerable<IEnumerable<string>> RawData
        {
            get
            {
                yield return new[] { "a", "b" } ;
                yield return new[] { "a", "b", "c" } ;
                yield return new[] { "a", "b", "c", "d" };
            }
        }


        public static IEnumerable<object[]> Data
        {
            get
            {
                return RawData.Select(collection => new [] { collection });
            }
        }

        public static IEnumerable<object[]> DataSquare
        {
            get
            {
                return RawData.Cartesian(RawData, (rw1, rw2) => new[] { rw1, rw2 });
            }
        }

        [Theory, PropertyData("Data")]
        public void UnionWith_Add_ElementsToCollection(string[] strings)
        {
            var PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.UnionWith(strings);
            bool res;

            Received.InOrder( () => 
                strings.ForEach(s => _LetterSimpleSetSubstitute.Add(s, out res))
                );
        }

        [Theory, PropertyData("DataSquare")]
        public void IntersectWith_Add_ElementsToCollection(string[] strings1, string[] strings2)
        {
            var set1 = new HashSet<string>(strings1);
            var set2 = new HashSet<string>(strings2);
            SetUp(set1);

            var PolyMorphSet = new PolyMorphSet<string>();
            PolyMorphSet.IntersectWith(strings2);
            set1.IntersectWith(set2);

            _LetterSimpleSetFactory
                .Received(1)
                .GetDefault(Arg.Is<IEnumerable<string>>(col => CheckCollection(col,set1)));
        }

        private bool CheckCollection(IEnumerable<string> col, IEnumerable<string> expected)
        {
            col.Should().BeEquivalentTo(expected); 
            return true; 
        }

        public void Dispose()
        {
            LetterSimpleSetFactory<string>.Factory = new LetterSimpleSetFactory<string>();
        }
    }
}
