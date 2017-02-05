namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class UnorderedDictionaryStrategy<TKey, TValue> : DictionaryStrategy<TKey, TValue> 
    {
        internal UnorderedDictionaryStrategy(int DictionaryTransition): base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableListDictionary<TKey, TValue>(current, this);
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection()
        {
            return new MutableListDictionary<TKey, TValue>(this);
        }
    }
}
