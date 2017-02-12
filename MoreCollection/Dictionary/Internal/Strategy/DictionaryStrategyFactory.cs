using System;
using System.Linq;
using System.Collections.Concurrent;

namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal static class DictionaryStrategyFactory 
    {
        private static readonly ConcurrentDictionary<Type, IDictionaryStrategy> _Strategies = new ConcurrentDictionary<Type, IDictionaryStrategy>();
        private static readonly Type IComparableType = typeof(IComparable<>);
        private const int _ListTransition = 10;
        private static IDictionaryStrategy _OrderedDictionaryStrategy = new OrderedDictionaryStrategy(_ListTransition);
        private static IDictionaryStrategy _UnorderedDictionaryStrategy = new UnorderedDictionaryStrategy(_ListTransition);

        private static bool IsComparable(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType
                            && interfaceType.GetGenericTypeDefinition() == IComparableType
                            && (interfaceType.GetGenericArguments()[0]) == type);
        }

        public static IDictionaryStrategy GetStrategy<TKey>()
        {
           return _Strategies.GetOrAdd(typeof(TKey), ComputeStrategy<TKey>());
        }

        private static IDictionaryStrategy ComputeStrategy<TKey>()
        {
            bool comparable = IsComparable(typeof(TKey));
            return comparable ? _OrderedDictionaryStrategy : _UnorderedDictionaryStrategy;
        }
    }
}
