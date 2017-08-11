using MoreCollection.Dictionary.Internal.Helper;

namespace MoreCollection.Dictionary.Internal.Strategy 
{
    internal class DictionaryStrategies
    {
        private const int _ListTransition = 10;
        internal static IDictionaryStrategy OrderedDictionaryStrategy { get; } = new OrderedDictionaryStrategy(_ListTransition);
        internal static IDictionaryStrategy UnorderedDictionaryStrategy { get; } = new UnorderedDictionaryStrategy(_ListTransition);
    }

    internal static class DictionaryStrategyFactory<TKey>
    {
        public static IDictionaryStrategy GetStrategy()
        {
            bool comparable = typeof(TKey).IsComparable();
            return comparable ? DictionaryStrategies.OrderedDictionaryStrategy : DictionaryStrategies.UnorderedDictionaryStrategy;
        }

        internal static IDictionaryStrategy Strategy { get; set; } = GetStrategy();
    }
}
