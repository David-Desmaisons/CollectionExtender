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

namespace CollectionExtenderTest.Extensions
{
    public class IEnumerableExtensionsTest
    {
        private Action<int> _Action;
        public IEnumerableExtensionsTest()
        {
            _Action = Substitute.For<Action<int>>();
        }

        [Fact]
        public void ForEach_CalledOnNull_ReturnNull()
        {
            IEnumerable<int> enumerable = null;
            var res = enumerable.ForEach(_Action);
            res.Should().BeNull();
            _Action.DidNotReceive().Invoke(Arg.Any<int>());
        }

        [Fact]
        public void ForEach_Call_Action_On_All_Elements()
        {
            IEnumerable<int> enumerable = Enumerable.Range(0,10);
            var res = enumerable.ForEach(_Action);
            Received.InOrder(() =>
            {
                foreach(var num in enumerable)
                {
                    _Action(num);
                }
            });
        }

        [Theory, PropertyData("Datas")]
        public void ForEach_Returns_CallingElement(IEnumerable<int> enumerable)
        {
            var res = enumerable.ForEach(_Action);
            res.Should().Equal(enumerable);
        }

        public static IEnumerable<object[]> Datas
        {
            get
            {
                IEnumerable<int> Null = null;
                yield return new[] { Null };
                yield return new []{Enumerable.Empty<int>()};
            }
        }
    }
}
