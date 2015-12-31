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
    public class IEnumerableExtensionsTest
    {
        private Action<int> _Action;
        public IEnumerableExtensionsTest()
        {
            _Action = Substitute.For<Action<int>>();
        }

        [Fact]
        public void ForEach_CalledOnNull_DoNotThrowException()
        {
            IEnumerable<int> enumerable = null;
            var res = enumerable.ForEach(_Action);
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
                yield return new []{ Enumerable.Empty<int>()};
                yield return new[] { new List<int>(){0,5,10} };
            }
        }


        [Fact]
        public void ForEachWithCancellation_CalledOnNull_DoNotThrowException()
        {
            IEnumerable<int> enumerable = null;
            var res = enumerable.ForEach(_Action, CancellationToken.None);
            _Action.DidNotReceive().Invoke(Arg.Any<int>());
        }

        [Fact]
        public void ForEachCancellation_Call_Action_On_All_Elements()
        {
            IEnumerable<int> enumerable = Enumerable.Range(0, 10);
            var res = enumerable.ForEach(_Action, CancellationToken.None);
            Received.InOrder(() =>
            {
                foreach (var num in enumerable)
                {
                    _Action(num);
                }
            });
        }

        [Theory, PropertyData("Datas")]
        public void ForEachCancellation_Returns_True_OnSuccess(IEnumerable<int> enumerable)
        {
            var res = enumerable.ForEach(_Action, CancellationToken.None);
            res.Should().BeTrue();
        }

        [Fact]
        public void ForEachCancellation_Call_Use_Cancellation()
        {
            IEnumerable<int> enumerable = Enumerable.Range(0, 10);
            CancellationTokenSource cts = new CancellationTokenSource();
            var called = new List<int>();
            Action<int> action = i => { called.Add(i); if (i == 3) cts.Cancel(); };
            var res = enumerable.ForEach(action, cts.Token);
            res.Should().BeFalse();
            called.Should().BeEquivalentTo(Enumerable.Range(0, 4));
        }
    }
}
