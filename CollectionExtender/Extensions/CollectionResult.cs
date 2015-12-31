using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Extensions
{
    public struct CollectionResult<T>
    {
        public T Item { get; set; }
        public CollectionStatus CollectionStatus { get; set; }
    }
}
