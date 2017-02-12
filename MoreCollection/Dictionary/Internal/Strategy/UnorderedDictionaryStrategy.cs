namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class UnorderedDictionaryStrategy<TKey> : DictionaryStrategy<TKey> 
    {
        internal UnorderedDictionaryStrategy(int DictionaryTransition): base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TValue>(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableListDictionary<TKey, TValue>(current, this);
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TValue>()
        {
            return new MutableListDictionary<TKey, TValue>(this);
        }
    }
}
