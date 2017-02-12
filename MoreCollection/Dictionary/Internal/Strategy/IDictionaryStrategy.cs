namespace MoreCollection.Dictionary.Internal.Strategy
{
    public interface IDictionaryStrategy
    {
        IMutableDictionary<TKey, TValue> GetEmpty<TKey, TValue>(int expectedCapacity = 0);
        IMutableDictionary<TKey, TValue> GetIntermediateCollection<TKey, TValue>(IMutableDictionary<TKey, TValue> current);
        IMutableDictionary<TKey, TValue> Add<TKey, TValue>(IMutableDictionary<TKey, TValue> current, TKey key, TValue value);
        IMutableDictionary<TKey, TValue> Remove<TKey, TValue>(IMutableDictionary<TKey, TValue> current, TKey key, out bool Result);
        IMutableDictionary<TKey, TValue> Update<TKey, TValue>(IMutableDictionary<TKey, TValue> current, TKey key, TValue value);
        IMutableDictionary<TKey, TValue> CheckDictionaryRemoved<TKey, TValue>(IMutableDictionary<TKey, TValue> current);
    }
}
