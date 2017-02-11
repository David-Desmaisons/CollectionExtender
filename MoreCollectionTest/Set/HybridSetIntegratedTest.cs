using MoreCollection.Set;
using Xunit;

namespace MoreCollectionTest.Set
{
    [Collection("Changing Default static stategy")]
    public class HybridSetIntegratedTest : SetTest
    {
        public HybridSetIntegratedTest(): base(new HybridSet<string>())
        {
        }
    }
}
