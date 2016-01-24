using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactoryBuilder : ILetterSimpleSetFactoryBuilder
    {
        static LetterSimpleSetFactoryBuilder()
        {
            Builder = new LetterSimpleSetFactoryBuilder();
        }

        public ILetterSimpleSetFactory<T> GetFactory<T>(int MaxList) where T: class 
        {
            return new LetterSimpleSetFactory<T>(MaxList);
        }

        internal static ILetterSimpleSetFactoryBuilder Builder
        {
            get;  set;
        }
    }
}
