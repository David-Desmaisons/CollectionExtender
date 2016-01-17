using CollectionExtender.Set;
using CollectionExtender.Set.Infra;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CollectionExtenderTest.Set
{
    public class PolyMorphSetTest : IDisposable
    {
        private ILetterSimpleSet<string> _LetterSimpleSetSubstitute;
        private ILetterSimpleSetFactory<string> _LetterSimpleSetFactory;
        private IEnumerable<string> _Enumerable;

        public PolyMorphSetTest()
        {
            _Enumerable = Substitute.For<IEnumerable<string>>();
            _LetterSimpleSetSubstitute = Substitute.For<ILetterSimpleSet<string>>();
            _LetterSimpleSetFactory = Substitute.For<ILetterSimpleSetFactory<string>>();
            _LetterSimpleSetFactory.GetDefault().Returns(_LetterSimpleSetSubstitute);
            _LetterSimpleSetFactory.GetDefault(Arg.Any<string>()).Returns(_LetterSimpleSetSubstitute);
            _LetterSimpleSetFactory.GetDefault(Arg.Any<IEnumerable<string>>()).Returns(_LetterSimpleSetSubstitute);
            LetterSimpleSetFactory<string>.Factory = _LetterSimpleSetFactory;
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

        public void Dispose()
        {
            LetterSimpleSetFactory<string>.Factory = new LetterSimpleSetFactory<string>();
        }
    }
}
