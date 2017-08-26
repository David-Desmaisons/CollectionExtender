using MoreCollection.Set;
using Xunit;

namespace MoreCollectionTest.Set
{
    [Collection("HashedSet Integrated Test")]
    public class HashedSetIntegratedTest : SetTest
    {
        public HashedSetIntegratedTest(): base(new HashedSet<string>())
        {
        }
    }
}
