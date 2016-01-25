namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal abstract class DictionaryStrategy<TKey, TValue> : IDictionaryStrategy<TKey,TValue>  where TKey : class
    {
        private readonly int _TransitionToDictionary;
        public DictionaryStrategy (int limit = 10)
        {
            _TransitionToDictionary = limit;
        }

        private IMutableDictionary<TKey, TValue> GetNext(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableDictionary<TKey, TValue>(current, this);
        }

        public IMutableDictionary<TKey, TValue> Add(IMutableDictionary<TKey, TValue> current, TKey key, TValue value)
        {
            if (current.Count < _TransitionToDictionary)
            {
                current.Add(key, value);
                return current;
            }

            return GetNext(current).AddMutable(key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(IMutableDictionary<TKey, TValue> current, TKey key, TValue value)
        {
            if ((current.Count == _TransitionToDictionary) && (!current.ContainsKey(key)))
            {
                return GetNext(current).AddMutable(key, value);
            }

            current[key] = value;
            return current;
        }

        public IMutableDictionary<TKey, TValue> Remove(IMutableDictionary<TKey, TValue> current, TKey key, out bool Result)
        {
            Result = current.Remove(key);

            if (current.Count == 1)
            {
                return new MutableSingleDictionary<TKey, TValue>(current, this);
            }

            return current;
        }

        public abstract IMutableDictionary<TKey, TValue> GetIntermediateCollection(IMutableDictionary<TKey, TValue> current);

        public IMutableDictionary<TKey, TValue> GetEmpty()
        {
            return new MutableSingleDictionary<TKey, TValue>(this);
        }

        public IMutableDictionary<TKey, TValue> CheckDictionaryRemoved(IMutableDictionary<TKey, TValue> current)
        {
            return (current.Count == _TransitionToDictionary) ? GetIntermediateCollection(current) : current;      
        }
    }
}
