using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal class Add : SetComandArgument
    {
        public Add(int parameter) : base(parameter)
        {
        }

        protected override void Perform(ISet<int> c, int parameter)
        {
            c.Add(parameter);
        }
    }
}
