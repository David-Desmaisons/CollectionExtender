using System;
using System.Collections.Generic;
using System.Linq;
using CollectionExtender.Extensions;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace CollectionExtenderTest.Extensions
{
    public class ListExtensionTest
    {
        private IList<int> List { get { return _List; } }
        private readonly List<int> _List;
        public ListExtensionTest()
        {
            _List = new List<int>();
        }

        [Theory, PropertyData("Data")]
        public void AddRange_AppendsElement(IEnumerable<int> enumerable)
        {
            var excepcted = new List<int>(enumerable ?? Enumerable.Empty<int>());
            var res = List.AddRange(enumerable);
            if (enumerable!=null)
                excepcted.AddRange(enumerable);
            List.Should().Equals(excepcted);
        }

        [Theory, PropertyData("Data")]
        public void AddRange_ReturnsCallingList(IEnumerable<int> enumerable)
        {
            var res = List.AddRange(enumerable);
            res.Should().Equals(List);
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                IEnumerable<int> nullcollection = null;
                yield return new object[] { Enumerable.Empty<int>() };
                yield return new object[] { new List<int>() { 0, 5, 10 } };
                yield return new object[] { nullcollection };
            }
        }

        [Fact]
        public void Addrange_CalledOnNull_ThrowException()
        {
            IList<int> list = null;
            Action Do = () => list.AddRange(Enumerable.Empty<int>() );
            Do.ShouldThrow<ArgumentNullException>();
        }
    }
}
