using MoreCollection.Set.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace MoreCollectionTest.Set.Internal
{
    public class LetterSimpleSetFactoryTest
    {
        private LetterSimpleSetFactory<string> _LetterSimpleSetFactory;
        public LetterSimpleSetFactoryTest()
        {
            _LetterSimpleSetFactory = new LetterSimpleSetFactory<string>();
        }


        [Fact]
        public void GetDefault_Return_SingleSet()
        {
            var res = _LetterSimpleSetFactory.GetDefault();
            res.Should().BeOfType<SingleSet<string>>();
        }

        [Fact]
        public void GetDefault_Return_EmptySet()
        {
            var res = _LetterSimpleSetFactory.GetDefault();
            res.Should().BeEmpty();
        }

        [Fact]
        public void GetDefault_T_Return_SingleSet()
        {
            var res = _LetterSimpleSetFactory.GetDefault("kkk");
            res.Should().BeOfType<SingleSet<string>>();
        }

        [Theory]
        [InlineData("one")]
        [InlineData("two")]
        [InlineData("three")]
        [InlineData("four")]
        public void GetDefault_T_Return_SetWithOneElement(string data)
        {
            var res = _LetterSimpleSetFactory.GetDefault(data);
            res.Should().BeEquivalentTo(new[] { data });
        }

        [Fact]
        public void GetDefault_IEnumerableT_Return_SingleSetNoElement()
        {
            var res = _LetterSimpleSetFactory.GetDefault(Enumerable.Empty<string>());
            res.Should().BeOfType<SingleSet<string>>();
        }

        [Fact]
        public void GetDefault_IEnumerableT_ThrowException_NullArgument()
        {
            IEnumerable<string> nullEnumerable = null;
            Action Do = () => _LetterSimpleSetFactory.GetDefault(nullEnumerable);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetDefault_IEnumerableT_Return_SingleSet_OneElement()
        {
            var res = _LetterSimpleSetFactory.GetDefault(new[] { "kkk" });
            res.Should().BeOfType<SingleSet<string>>();
        }

        [Fact]
        public void GetDefault_IEnumerableT_Return_ListSet_TwoElement()
        {
            var res = _LetterSimpleSetFactory.GetDefault(new[] { "kkk", "lll" });
            res.Should().BeOfType<ListSet<string>>();
        }

        [Fact]
        public void GetDefault_IEnumerableT_Return_HashSet_MoreElementsThanLimit()
        {
            LetterSimpleSetFactory<string>.MaxList = 4;
            var res = _LetterSimpleSetFactory.GetDefault(new[] { "kkk", "lll", "kkkp", "lllp" });
            res.Should().BeOfType<SimpleHashSet<string>>();
        }

        [Fact]
        public void GetDefault_RemoveDuplicate()
        {
            LetterSimpleSetFactory<string>.MaxList = 4;
            var res = _LetterSimpleSetFactory.GetDefault(new[] { "kkk", "lll", "lll", "lll", "kkk" });
            res.Should().BeEquivalentTo(new[] { "kkk", "lll"});
        }

        [Fact]
        public void Factory_Return_LetterSimpleSetFactory()
        {
            var res = LetterSimpleSetFactory<string>.Factory;
            res.Should().BeOfType<LetterSimpleSetFactory<string>>();
        }
    }
}
