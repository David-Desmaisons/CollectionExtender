namespace MoreCollection.Dictionary.Internal.Strategy
{
    public interface IDictionaryStrategy<TKey, TValue>
    {       
        IMutableDictionary<TKey, TValue> GetEmpty(int expectedCapacity=0);
        IMutableDictionary<TKey, TValue> GetIntermediateCollection(IMutableDictionary<TKey, TValue> current);
        IMutableDictionary<TKey, TValue> Add(IMutableDictionary<TKey, TValue> current, TKey key, TValue value);
        IMutableDictionary<TKey, TValue> Remove(IMutableDictionary<TKey, TValue> current, TKey key, out bool Result);
        IMutableDictionary<TKey, TValue> Update(IMutableDictionary<TKey, TValue> current, TKey key, TValue value);
        IMutableDictionary<TKey, TValue> CheckDictionaryRemoved(IMutableDictionary<TKey, TValue> current);
    }
}
