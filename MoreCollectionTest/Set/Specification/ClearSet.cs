using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal class ClearSet : SetComand
    {
        protected override void Perform(ISet<int> c)
        {
            c.Clear();
        }
    }
}
