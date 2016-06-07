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
    }
}
