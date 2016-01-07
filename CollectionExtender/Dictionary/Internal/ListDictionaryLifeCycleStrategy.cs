//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CollectionExtender.Dictionary.Internal
//{
//    internal class ListDictionaryLifeCycleStrategy<Tkey, TValue> : DictionaryLifeCycleStrategy<Tkey, TValue>
//    {
//        internal ListDictionaryLifeCycleStrategy(int transitionToDictionary = 25)
//            : base(transitionToDictionary)
//        {
//        }

//        protected override IDictionary<Tkey, TValue> BuildListBasedDictionary(IDictionary<Tkey, TValue> source)
//        {
//            return new ListDictionary<Tkey, TValue>(source);
//        }
//    }
//}
