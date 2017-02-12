namespace MoreCollection.Dictionary.Internal.Strategy
{
    public interface IDictionaryStrategy<TKey>
    {       
        IMutableDictionary<TKey, TValue> GetEmpty<TValue>(int expectedCapacity=0);
        IMutableDictionary<TKey, TValue> GetIntermediateCollection<TValue>(IMutableDictionary<TKey, TValue> current);
        IMutableDictionary<TKey, TValue> Add<TValue>(IMutableDictionary<TKey, TValue> current, TKey key, TValue value);
        IMutableDictionary<TKey, TValue> Remove<TValue>(IMutableDictionary<TKey, TValue> current, TKey key, out bool Result);
        IMutableDictionary<TKey, TValue> Update<TValue>(IMutableDictionary<TKey, TValue> current, TKey key, TValue value);
        IMutableDictionary<TKey, TValue> CheckDictionaryRemoved<TValue>(IMutableDictionary<TKey, TValue> current);
    }
}
