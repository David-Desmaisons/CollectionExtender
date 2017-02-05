using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal class Remove : SetComandArgument
    {
        public Remove(int parameter) : base(parameter)
        {
        }

        protected override void Perform(ISet<int> c, int parameter)
        {
            c.Remove(parameter);
        }
    }
}
