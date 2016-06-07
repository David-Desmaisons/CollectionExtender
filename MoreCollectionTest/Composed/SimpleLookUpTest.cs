using MoreCollection.Composed;
using FluentAssertions;
using Xunit;

namespace MoreCollectionTest.Composed
{
    public class SimpleLookUpTest
    {
        private readonly SimpleLookUp<string, int> _Lookup;
        public SimpleLookUpTest()
        {
            _Lookup = new SimpleLookUp<string, int>();
        }

        [Fact]
        public void New_IsEmpty()
        {
            _Lookup.Should().BeEmpty();
        }

        [Theory]
        [InlineData("a", 1)]
        [InlineData("b", 1)]
        [InlineData("c", 34)]
        public void Add_AddAnElement(string key, int value) 
        {
            _Lookup.Add(key, value);
            _Lookup[key].Should().BeEquivalentTo(new[] {value});
        }
    }
}
