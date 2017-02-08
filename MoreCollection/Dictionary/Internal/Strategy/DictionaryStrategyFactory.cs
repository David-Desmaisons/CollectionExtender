using System;
using System.Collections.Generic;
using System.Linq;
using MoreCollection.Extensions;

namespace MoreCollection.Dictionary.Internal.Strategy
{
    internal static class DictionaryStrategyFactory 
    {
        private static readonly Dictionary<Type, bool> _IsComparable = new Dictionary<Type, bool>();
        private static readonly Type IComparableType = typeof(IComparable<>);

        private static bool IsComparable(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType
                            && interfaceType.GetGenericTypeDefinition() == IComparableType
                            && (interfaceType.GetGenericArguments()[0]) == type);
        }

        public static IDictionaryStrategy<TKey, TValue> GetStrategy<TKey, TValue>(int ListTransition)
        {
            bool comparable = _IsComparable.GetOrAddEntity(typeof(TKey), IsComparable);

            if (comparable)
                return new OrderedDictionaryStrategy<TKey, TValue>(ListTransition);

            return new UnorderedDictionaryStrategy<TKey, TValue>(ListTransition);
        }
    }
}
