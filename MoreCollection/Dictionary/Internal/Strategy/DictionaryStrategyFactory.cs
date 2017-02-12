using System;
using System.Linq;
using System.Collections.Concurrent;

namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal static class DictionaryStrategyFactory 
    {
        private static readonly ConcurrentDictionary<Type, object> _Strategies = new ConcurrentDictionary<Type, object>();
        private static readonly Type IComparableType = typeof(IComparable<>);
        private const int _ListTransition = 10;

        private static bool IsComparable(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType
                            && interfaceType.GetGenericTypeDefinition() == IComparableType
                            && (interfaceType.GetGenericArguments()[0]) == type);
        }

        public static IDictionaryStrategy<TKey> GetStrategy<TKey>()
        {
           return (DictionaryStrategy<TKey>) _Strategies.GetOrAdd(typeof(TKey), ComputeStrategy<TKey>());
        }

        private static IDictionaryStrategy<TKey> ComputeStrategy<TKey>()
        {
            bool comparable = IsComparable(typeof(TKey));

            if (comparable)
                return new OrderedDictionaryStrategy<TKey>(_ListTransition);

            return new UnorderedDictionaryStrategy<TKey>(_ListTransition);
        }
    }
}
