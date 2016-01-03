using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Dictionary.Internal
{
    public interface IDictionaryLifeCycleStrategy<Tkey, TValue>
    {
        void Add(ref IDictionary<Tkey, TValue> dictionary, Tkey key, TValue value);

        bool Remove(ref IDictionary<Tkey, TValue> dictionary, Tkey key);

        void Update(ref IDictionary<Tkey, TValue> dictionary, Tkey key, TValue value);
    }
}
