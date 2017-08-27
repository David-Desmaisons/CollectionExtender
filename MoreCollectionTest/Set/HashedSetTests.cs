using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using MoreCollection.Set;
using Xunit;
using Xunit.Abstractions;

namespace MoreCollectionTest.Set
{
    public class HashedSetTests
    {
        private readonly ITestOutputHelper _Output;
        private const int Size = 5000;
        private const int Operations = 100000;

        public HashedSetTests(ITestOutputHelper output)
        {
            _Output = output;
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new[] { 1 })]
        [InlineData(new[] { 1, 2 })]
        [InlineData(new[] { 4, 3, 5, 2 })]
        public void Clone_CopyAllElements(int[] elements)
        {
            var set = (elements == null) ? new HashedSet<int>() : new HashedSet<int>(elements);
            var cloned = set.Clone();

            set.Should().BeEquivalentTo(cloned);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new[] { 1 })]
        [InlineData(new[] { 1, 2 })]
        [InlineData(new[] { 4, 3, 5, 2 })]
        public void Clone_IsShallow(int[] elements)
        {
            var set = (elements == null) ? new HashedSet<int>() : new HashedSet<int>(elements);
            var originalCount = set.Count;
            var cloned = set.Clone();
            cloned.Add(10);

            set.Should().HaveCount(originalCount);
            cloned.Should().HaveCount(originalCount + 1);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(50)]
        [InlineData(500)]
        [InlineData(5000)]
        [InlineData(10000)]
        public void Performance_Copy_HashSet_vs_Clone_HashedSet(int size)
        {
            var stopWatch = new Stopwatch();
            
            var elements = Enumerable.Range(0, size).ToList();
            var hashSet = new HashSet<int>(elements);
            var hashedSet = new HashedSet<int>(elements);

            stopWatch.Start();
            for (var i = 0; i < Operations; i++)
            {
                var newSet = new HashSet<int>(hashSet);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            stopWatch.Stop();
            var ts = stopWatch.ElapsedMilliseconds;
            _Output.WriteLine($"Perf: {Operations * 1000 / ts} operations per sec new HashSet");

            stopWatch.Reset();
            stopWatch.Start();
            for (var i = 0; i < Operations; i++)
            {
                var newSet = hashedSet.Clone();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            stopWatch.Stop();
            ts = stopWatch.ElapsedMilliseconds;
            _Output.WriteLine($"Perf: {Operations * 1000 / ts} operations per sec new hashedSet");
        }
    }
}
