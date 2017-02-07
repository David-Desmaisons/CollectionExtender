using System.Collections.Generic;

namespace MoreCollectionTest.Dictionary.Specification
{
    internal class ClearDictionary : DictionaryComand
    {
        protected override void Perform(IDictionary<int, string> c)
        {
            c.Clear();
        }
    }
}
