using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionExtender.Extensions;
using Xunit;
using FluentAssertions;
using NSubstitute;
using Xunit.Extensions;
using System.Threading;

namespace CollectionExtenderTest.Extensions
{
    public class IListExtensionTest
    {
        private IList<int> List { get { return _List; } }
        private List<int> _List;
        public IListExtensionTest()
        {
            _List = new List<int>();
        }

        //[Fact]
        //public void ForEach_CalledOnNull_DoNotThrowException()
        //{
        //    var res = List.AddRangeI
        //}

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
                yield return new[] { Enumerable.Empty<int>() };
                yield return new[] { new List<int>() { 0, 5, 10 } };
                yield return new[] { nullcollection };
            }
        }

        [Fact]
        public void Addrange_CalledOnNull_ThrowException()
        {
            IList<int> list = null;
            Action Do = () => list.AddRange(Enumerable.Empty<int>() );
            Do.ShouldThrow<NullReferenceException>();
        }
    }
}
