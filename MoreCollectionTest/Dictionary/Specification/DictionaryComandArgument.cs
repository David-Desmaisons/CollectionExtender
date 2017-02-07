using System.Collections.Generic;

namespace MoreCollectionTest.Dictionary.Specification
{
    internal abstract class DictionaryComandArgument<T> : DictionaryComand
    {
        protected abstract void Perform(IDictionary<int, string> c, T parameter);

        private readonly T _Parameter;
        protected DictionaryComandArgument(T parameter)
        {
            _Parameter = parameter;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {_Parameter}";
        }

        protected override void Perform(IDictionary<int, string> set)
        {
            Perform(set, _Parameter);
        }
    }
}
