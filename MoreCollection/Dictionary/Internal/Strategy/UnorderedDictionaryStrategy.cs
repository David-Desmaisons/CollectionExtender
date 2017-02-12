namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal class UnorderedDictionaryStrategy : DictionaryStrategy
    {
        internal UnorderedDictionaryStrategy(int DictionaryTransition): base(DictionaryTransition)
        {
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TKey, TValue>(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableListDictionary<TKey, TValue>(current, this);
        }

        public override IMutableDictionary<TKey, TValue> GetIntermediateCollection<TKey,TValue>()
        {
            return new MutableListDictionary<TKey, TValue>(this);
        }
    }
}
