using MoreCollection.Extensions;
using MoreCollection.Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollection.Dictionary.Internal.Helper
{
    internal static class Comparable 
    {
        private static readonly IDictionary<Type, bool> _Comparable = new Dictionary<Type, bool>();

        private static readonly Type IComparableType = typeof(IComparable<>);

        private static bool PrivateIsComparable(Type type) 
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGeneric()
                            && interfaceType.GetGenericTypeDefinition() == IComparableType
                            && (interfaceType.GetFirstGenericArguments() == type));
        }

        internal static bool IsComparable(this Type type) 
        {
            return _Comparable.GetOrAddEntity(type, PrivateIsComparable);
        }
    }
}
