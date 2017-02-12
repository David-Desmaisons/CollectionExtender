namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class OrderedDictionaryStrategy :   DictionaryStrategy
    {
        internal OrderedDictionaryStrategy(int DictionaryTransition):base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TKey, TValue>(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableSortedListDictionary<TKey, TValue>(current, this);
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TKey, TValue>()
        {
            return new MutableSortedListDictionary<TKey, TValue>(this);
        }
    }
}
