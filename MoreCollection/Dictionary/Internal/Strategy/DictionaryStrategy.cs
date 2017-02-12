namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal abstract class DictionaryStrategy : IDictionaryStrategy
    {
        private readonly int _TransitionToDictionary;
        public DictionaryStrategy (int limit = 10)
        {
            _TransitionToDictionary = limit;
        }

        private IMutableDictionary<TKey, TValue> GetNext<TKey, TValue>(IMutableDictionary<TKey, TValue> current)
        {
            return new MutableDictionary<TKey, TValue>(current, this);
        }

        public IMutableDictionary<TKey, TValue> Add<TKey,TValue>(IMutableDictionary<TKey, TValue> current, TKey key, TValue value)
        {
            if (current.Count < _TransitionToDictionary)
            {
                current.Add(key, value);
                return current;
            }

            return GetNext(current).AddMutable(key, value);
        }

        public IMutableDictionary<TKey, TValue> Update<TKey, TValue>(IMutableDictionary<TKey, TValue> current, TKey key, TValue value)
        {
            if ((current.Count == _TransitionToDictionary) && (!current.ContainsKey(key)))
            {
                return GetNext(current).AddMutable(key, value);
            }

            current[key] = value;
            return current;
        }

        public IMutableDictionary<TKey, TValue> Remove<TKey, TValue>(IMutableDictionary<TKey, TValue> current, TKey key, out bool Result)
        {
            Result = current.Remove(key);

            if (current.Count == 1)
            {
                return new MutableSingleDictionary<TKey, TValue>(current, this);
            }

            return current;
        }

        public abstract IMutableDictionary<TKey, TValue> GetIntermediateCollection<TKey, TValue>(IMutableDictionary<TKey, TValue> current);

        public abstract IMutableDictionary<TKey, TValue> GetIntermediateCollection<TKey, TValue>();

        public IMutableDictionary<TKey, TValue> GetEmpty<TKey, TValue>(int expectedCapacity=0)
        {
            if (expectedCapacity<2)
                return new MutableSingleDictionary<TKey, TValue>(this);

            if (expectedCapacity <= _TransitionToDictionary)
                return GetIntermediateCollection<TKey, TValue>();

            return new MutableDictionary<TKey, TValue>(this);
        }

        public IMutableDictionary<TKey, TValue> CheckDictionaryRemoved<TKey, TValue>(IMutableDictionary<TKey, TValue> current)
        {
            return (current.Count == _TransitionToDictionary) ? GetIntermediateCollection(current) : current;      
        }
    }
}
