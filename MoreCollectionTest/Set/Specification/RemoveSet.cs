using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal class RemoveSet : SetComandArgument
    {
        public RemoveSet(int parameter) : base(parameter)
        {
        }

        protected override void Perform(ISet<int> c, int parameter)
        {
            c.Remove(parameter);
        }
    }
}
