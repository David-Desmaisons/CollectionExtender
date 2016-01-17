using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Set
{
    public class HashedSet<T> : HashSet<T>, ISet<T> where T : class
    {
    }
}
