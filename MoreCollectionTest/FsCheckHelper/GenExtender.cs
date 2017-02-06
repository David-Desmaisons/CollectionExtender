using FsCheck;
using System.Collections.Generic;
using System.Linq;

namespace MoreCollectionTest.FsCheckHelper
{
    public static class GenExtender
    {
        public static IEnumerable<T> Generate<T>(this Gen<T> self, int number)
        {
            return Gen.Sample(0, number, self);
        }

        public static T Generate<T>(this Gen<T> self)
        {
            return Generate<T>(self, 1).First();
        }
    }
}
