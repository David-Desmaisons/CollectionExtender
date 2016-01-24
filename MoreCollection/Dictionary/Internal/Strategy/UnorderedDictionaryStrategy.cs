using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class UnorderedDictionaryStrategy<TKey, TValue> : DictionaryStrategy<TKey, TValue> 
                        where TKey : class
    {
        internal UnorderedDictionaryStrategy(int DictionaryTransition): base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableListDictionary<TKey, TValue>(current, this);
        }
    }
}
