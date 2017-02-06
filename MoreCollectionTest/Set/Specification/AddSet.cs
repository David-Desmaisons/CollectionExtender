using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal class AddSet : SetComandArgument
    {
        public AddSet(int parameter) : base(parameter)
        {
        }

        protected override void Perform(ISet<int> c, int parameter)
        {
            c.Add(parameter);
        }
    }
}
