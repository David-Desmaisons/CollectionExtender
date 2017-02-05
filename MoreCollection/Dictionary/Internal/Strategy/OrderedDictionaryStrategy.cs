namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class OrderedDictionaryStrategy<TKey, TValue> :   DictionaryStrategy<TKey, TValue> 
    {
        internal OrderedDictionaryStrategy(int DictionaryTransition):base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableSortedDictionary<TKey, TValue>(current, this);
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection()
        {
            return new MutableSortedDictionary<TKey, TValue>(this);
        }
    }
}
