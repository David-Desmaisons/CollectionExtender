using MoreCollection.Set;
using Xunit;

namespace MoreCollectionTest.Set
{
    [Collection("Changing Default static set stategy")]
    public class HybridSetIntegratedTest : SetTest
    {
        public HybridSetIntegratedTest(): base(new HybridSet<string>())
        {
        }
    }
}
