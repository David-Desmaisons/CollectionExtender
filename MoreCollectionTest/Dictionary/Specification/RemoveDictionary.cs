using System.Collections.Generic;

namespace MoreCollectionTest.Dictionary.Specification
{
    internal class RemoveDictionary : DictionaryComandArgument<int>
    {
        public RemoveDictionary(int parameter) : base(parameter)
        {
        }

        protected override void Perform(IDictionary<int, string> c, int parameter)
        {
            c.Remove(parameter);
        }
    }
}
