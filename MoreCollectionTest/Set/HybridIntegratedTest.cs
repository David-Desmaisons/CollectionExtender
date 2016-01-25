using MoreCollection.Set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollectionTest.Set
{
    public class HybridIntegratedTest : SetTest
    {
        public HybridIntegratedTest()
        {
            _Set = new HybridSet<string>(3);
        }
    }
}
