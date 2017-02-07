using System;
using System.Collections.Generic;

namespace MoreCollectionTest.Dictionary.Specification
{
    internal class AddDictionary : DictionaryComandArgument<Tuple<int, string>>
    {
        public AddDictionary(Tuple<int, string> parameter) : base(parameter)
        {
        }

        protected override void Perform(IDictionary<int, string> c, Tuple<int, string> parameter)
        {
            c[parameter.Item1] = parameter.Item2;
        }
    }
}
