using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreCollection.Extensions;

namespace MoreCollection.Dictionary.Internal
{
    public class MutableDictionaryFactory
    {
        private static readonly Dictionary<Type, bool> _IsComparable = new Dictionary<Type, bool>();
        private static readonly Type IComparableType = typeof (IComparable<>);

        private static bool IsComparable(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType
                            && interfaceType.GetGenericTypeDefinition() == IComparableType
                            && (interfaceType.GetGenericArguments()[0]) == type);
        }

        public static IMutableDictionary<TKey, TValue> GetDefault<TKey, TValue> (int TransitionToDictionary = 25)
            where TKey: class
        {
            bool comparable = _IsComparable.FindOrCreateEntity(typeof(TKey), IsComparable);

            var targetMiddle = comparable ? typeof(MutableSortedDictionary<TKey, TValue>) : typeof(MutableListDictionary<TKey, TValue>);

            return new MutableSingleDictionary<TKey, TValue>(targetType: targetMiddle, transition: TransitionToDictionary);          
        }
    }
}
