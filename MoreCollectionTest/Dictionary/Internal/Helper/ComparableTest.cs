using FluentAssertions;
using MoreCollection.Dictionary.Internal.Helper;
using System;
using Xunit;

namespace MoreCollectionTest.Dictionary.Internal.Helper 
{
    public class ComparableTest 
    {
        [Theory]
        [InlineData(typeof(bool), true)]
        [InlineData(typeof(string), true)]
        [InlineData(typeof(decimal), true)]
        [InlineData(typeof(object), false)]
        [InlineData(typeof(ComparableTest), false)]
        public void IsComparable_returns_correct_value(Type type, bool expectedIsComparable)
        {
            type.IsComparable().Should().Be(expectedIsComparable);
        }
    }
}
