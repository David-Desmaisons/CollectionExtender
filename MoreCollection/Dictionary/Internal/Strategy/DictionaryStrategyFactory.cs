using System;
using System.Linq;

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
        private static readonly Type IComparableType = typeof(IComparable<>);

        internal static IDictionaryStrategy Strategy { get; set; } = GetStrategy();

        private static bool IsComparable(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType
                            && interfaceType.GetGenericTypeDefinition() == IComparableType
                            && (interfaceType.GetGenericArguments()[0]) == type);
        }

        public static IDictionaryStrategy GetStrategy()
        {
            bool comparable = IsComparable(typeof(TKey));
            return comparable ? DictionaryStrategies.OrderedDictionaryStrategy : DictionaryStrategies.UnorderedDictionaryStrategy;
        }
    }
}
