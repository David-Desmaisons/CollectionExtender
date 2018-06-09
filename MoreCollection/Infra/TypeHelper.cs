using System;
using System.Collections.Generic;
using System.Reflection;

namespace MoreCollection.Infra
{
    public static class TypeHelper 
    {
        public static IEnumerable<Type> GetInterfaces(this Type type) 
        {
#if NET45
            return type.GetInterfaces();
#else
            return type.GetTypeInfo().ImplementedInterfaces;
#endif
        }
        public static bool IsGeneric(this Type type) 
        {
#if NET45
            return type.IsGenericType;
#else
            return type.GetTypeInfo().IsGenericType;
#endif
        }

        public static Type GetFirstGenericArguments(this Type type) {
#if NET45
            return type.GetGenericArguments()[0];
#else
            return type.GetTypeInfo().GenericTypeArguments[0];
#endif
        }
    }
}
