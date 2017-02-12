namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class OrderedDictionaryStrategy<TKey> :   DictionaryStrategy<TKey> 
    {
        internal OrderedDictionaryStrategy(int DictionaryTransition):base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TValue>(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableSortedListDictionary<TKey, TValue>(current, this);
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TValue>()
        {
            return new MutableSortedListDictionary<TKey, TValue>(this);
        }
    }
}
