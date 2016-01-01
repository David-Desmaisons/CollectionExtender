using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Extensions
{
    public static class IDictionaryExtension

    {
        public static CollectionResult<TValue> FindOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> Fac)
        {
            if (dic == null)
                throw new ArgumentNullException("enumerable");

            TValue res = default(TValue);
            if (dic.TryGetValue(key, out res))
                return new CollectionResult<TValue>() { Item = res, CollectionStatus = CollectionStatus.Found };

            res = Fac(key);
            dic.Add(key, res);
            return new CollectionResult<TValue>() { Item = res, CollectionStatus = CollectionStatus.Created };
        }

        public static TValue FindOrCreateEntity<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> Fac)
        {
            if (dic == null)
                throw new ArgumentNullException("enumerable");

            TValue res = default(TValue);
            if (dic.TryGetValue(key, out res))
                return res;

            res = Fac(key);
            dic.Add(key, res);
            return res;
        }

        public static TValue UpdateOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key,  Func<TKey, TValue> Fac,
                                                                Func<TKey, TValue, TValue> Updater)
        {
            if (dic == null)
                throw new ArgumentNullException("enumerable");

            TValue res = default(TValue);
            if (dic.TryGetValue(key, out res))
            {
                TValue nv = Updater(key, res);
                dic[key] = nv;
                return nv;
            }

            res = Fac(key);
            dic.Add(key, res);
            return res;
        }

        public static TValue UpdateOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> Fac,
                                                                Action<TKey, TValue> Updater)
        {
            return IDictionaryExtension.UpdateOrCreate(dic, key, Fac, (k, v) => { Updater(k, v); return v; });
        }

        public static IDictionary<TKey, TValue> Import<TKey, TValue>(this IDictionary<TKey, TValue> dic, IDictionary<TKey, TValue> source)
        {
            if (dic == null)
                throw new ArgumentNullException("enumerable");

            source.ForEach(el => dic.Add(el.Key, el.Value));
            return dic;
        }
    }
}
