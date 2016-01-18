using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Dictionary.Internal
{
    public class MutableDictionaryFactory<TKey, TValue> where TKey: class
    {
        public static IMutableDictionary<TKey, TValue> GetDefault(int TransitionToDictionary = 25)
        {
            bool comparable = typeof(TKey).GetInterfaces()
                                    .Where(i => i.IsGenericType 
                                            && i.GetGenericTypeDefinition() == typeof(IComparable<>)
                                            && (i.GetGenericArguments()[0]) == typeof(TKey)).Any();

            var targetMiddle = comparable ? typeof(MutableSortedDictionary<TKey, TValue>) : typeof(MutableListDictionary<TKey, TValue>);

            return new MutableSingleDictionary<TKey, TValue>(targetType: targetMiddle, transition: TransitionToDictionary);          
        }
    }
}
