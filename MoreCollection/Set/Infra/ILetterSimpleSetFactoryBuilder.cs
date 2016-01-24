using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Set.Infra
{
    internal interface ILetterSimpleSetFactoryBuilder
    {
        ILetterSimpleSetFactory<T> GetFactory<T>(int MaxList) where T: class;
    }
}
