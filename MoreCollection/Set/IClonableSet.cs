using System.Collections.Generic;

namespace MoreCollection.Set
{
    public interface IClonableSet<T>: ISet<T>
    {
        IClonableSet<T> Clone();
    }
}
