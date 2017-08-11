using MoreCollection.Dictionary.Internal;
using MoreCollection.Dictionary.Internal.Helper;
using System.Collections.Generic;

namespace MoreCollection.Dictionary 
{
    public class DictionaryFactory 
    {
        private const int _ListTransition = 9;

        public static IDictionary<TKey, TValue> Get<TKey, TValue>(int expectedSize) 
        {
            if (expectedSize> _ListTransition)
                return new Dictionary<TKey, TValue>();

            return (typeof(TKey).IsComparable()) ? (IDictionary<TKey, TValue>)new SortedList<TKey, TValue>() : new ListDictionary<TKey, TValue>();
        }
    }
}
