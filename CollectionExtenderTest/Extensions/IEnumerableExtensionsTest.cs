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
        private IEnumerable<int> _NullEnumerable = null;
        private IEnumerable<int> _Enumerable;
        public IEnumerableExtensionsTest()
        {
            _Action = Substitute.For<Action<int>>();
            _Enumerable = Enumerable.Range(0, 10);
        }

        [Fact]
        public void ForEach_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.ForEach(_Action);
            Do.ShouldThrow<ArgumentNullException>();
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

        [Theory, PropertyData("Data")]
        public void ForEach_Returns_CallingElement(IEnumerable<int> enumerable)
        {
            var res = enumerable.ForEach(_Action);
            res.Should().Equal(enumerable);
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new []{ Enumerable.Empty<int>()};
                yield return new[] { new List<int>(){0,5,10} };
            }
        }

        [Fact]
        public void ForEachWithCancellation_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.ForEach(_Action, CancellationToken.None);
            Do.ShouldThrow<ArgumentNullException>();
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

        [Theory, PropertyData("Data")]
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

        [Fact]
        public void FirstOrDefault_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.FirstOrDefault(78, _ => true);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void FirstOrDefault_CalledWithNullPredicate_ThrowException()
        {
            Action Do = () => _Enumerable.FirstOrDefault(78, null);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void FirstOrDefault_ReturnFirstElement_WhenFound(int element)
        {
            Func<int, bool> predicate = i => i == element;
            var res = _Enumerable.FirstOrDefault(78, predicate);
            res.Should().Be(element);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        public void FirstOrDefault_ReturnArgument_IfNothingFound(int defaultValue)
        {
            var res = _Enumerable.FirstOrDefault(defaultValue, i => i == 10);
            res.Should().Be(defaultValue);
        }

        [Fact]
        public void FirstOrDefault2_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.FirstOrDefault(78);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void FirstOrDefault2_ReturnFirstElement_WhenFound(int element)
        {
            var enumerable = Enumerable.Range(element, 10);
            var res = enumerable.FirstOrDefault(78);
            res.Should().Be(element);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        public void FirstOrDefault2_ReturnArgument_IfNothingFound(int defaultValue)
        {
            var res = Enumerable.Empty<int>().FirstOrDefault(defaultValue);
            res.Should().Be(defaultValue);
        }

        [Fact]
        public void Indexes_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.Indexes(_ => true);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Indexes_ReturnsIndexes()
        {
           var res = _Enumerable.Indexes(i => i%2 ==0);
           res.Should().Equal(new [] {0,2,4,6,8});
        }

        [Fact]
        public void Indexes_ReturnsIndexes_2()
        {
            var res = _Enumerable.Indexes(i => i == 20);
            res.Should().BeEmpty();
        }

        [Fact]
        public void Indexes2_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.Indexes(33);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Indexes2_ReturnsIndexes(int element)
        {
            var res = _Enumerable.Indexes(element);
            res.Should().Equal(new[] { element });
        }

        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        public void Indexes2_ReturnsIndexes_2(int element)
        {
            var res = _Enumerable.Indexes(element);
            res.Should().BeEmpty();
        }

        [Fact]
        public void Index_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.Index(78);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Index_ReturnsIndexValue_IfItemFound(int element)
        {
            var res = _Enumerable.Index(element);
            res.Should().Be(element);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        public void Index_ReturnsMinusOne_IfNothingFound(int element)
        {
            var res = _Enumerable.Index(element);
            res.Should().Be(-1);
        }

        [Fact]
        public void Index2_CalledOnNull_ThrowException()
        {
            Action Do = () => _NullEnumerable.Index( _ => true);
            Do.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Index2_ReturnsIndexValue_IfItemFound(int element)
        {
            var res = _Enumerable.Index(i => i==element);
            res.Should().Be(element);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        public void Index2_ReturnsMinusOne_IfNothingFound(int element)
        {
            var res = _Enumerable.Index(i=> i==element);
            res.Should().Be(-1);
        }
    }
}
