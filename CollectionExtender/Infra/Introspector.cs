using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Infra
{
    internal static class Introspector
    {
        public static T Build<T>(params object[] constructorParameters) where T : class
        {
            return Activator.CreateInstance(typeof(T), constructorParameters) as T;
        }
    }
}
