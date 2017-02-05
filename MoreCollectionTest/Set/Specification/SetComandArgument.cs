using System.Collections.Generic;

namespace MoreCollectionTest.Set.Specification
{
    internal abstract class SetComandArgument  : SetComand
    {
        protected abstract void Perform(ISet<int> c, int parameter);

        private readonly int _Parameter;
        protected SetComandArgument(int parameter)
        {
            _Parameter = parameter;
        }

        protected override void Perform(ISet<int> set)
        {
            Perform(set, _Parameter);
        }

        public override string ToString()
        {
            return $"{base.ToString()} {_Parameter}";
        }
    }
}
