using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Infra
{
    internal static class Introspector
    {
        public static T Build<T>(params object[] constructorParameters) where T : class
        {
            return Activator.CreateInstance(typeof(T), constructorParameters) as T;
        }

        public static T BuildInstance<T>(Type type, params object[] constructorParameters) where T : class
        {
            return Activator.CreateInstance(type, constructorParameters) as T;
        }
    }
}
