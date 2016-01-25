namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class OrderedDictionaryStrategy<TKey, TValue> :   DictionaryStrategy<TKey, TValue> 
                        where TKey : class
    {
        internal OrderedDictionaryStrategy(int DictionaryTransition):base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableSortedDictionary<TKey, TValue>(current, this);
        }
    }
}
