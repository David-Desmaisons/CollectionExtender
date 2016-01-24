using MoreCollection.Dictionary.Internal.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreCollection.Dictionary.Internal
{
    public class MutableListDictionary<TKey, TValue> : ListDictionary<TKey, TValue>, IMutableDictionary<TKey, TValue> where TKey : class                              
    {
        private readonly IDictionaryStrategy<TKey, TValue> _DictionarySwitcher;
        public MutableListDictionary(IDictionaryStrategy<TKey, TValue> switcher)
        {
            _DictionarySwitcher = switcher;
        }

        public MutableListDictionary(IDictionary<TKey, TValue> collection, IDictionaryStrategy<TKey, TValue> switcher)
            : base(collection)
        {
            _DictionarySwitcher = switcher;
        }

        IMutableDictionary<TKey, TValue> IMutableDictionary<TKey, TValue>.AddMutable(TKey key, TValue value)
        {
            return _DictionarySwitcher.Add(this, key, value);
        }

        public IMutableDictionary<TKey, TValue> Update(TKey key, TValue value)
        {
            return _DictionarySwitcher.Update(this,  key, value);
        }

        public IMutableDictionary<TKey,TValue> Remove(TKey key, out bool result)
        {
            return _DictionarySwitcher.Remove(this,  key, out result);
        }

        public IMutableDictionary<TKey, TValue> ClearMutable()
        {
            return _DictionarySwitcher.GetEmpty();
        }
    }
}
