//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CollectionExtender.Dictionary.Internal
//{
//    internal class SortedDictionaryLifeCycleStrategy<Tkey, TValue> : DictionaryLifeCycleStrategy<Tkey, TValue>
//    {
//        internal SortedDictionaryLifeCycleStrategy(int transitionToDictionary = 25)
//            : base(transitionToDictionary)
//        {
//        }

//        protected override IDictionary<Tkey, TValue> BuildListBasedDictionary(IDictionary<Tkey, TValue> source)
//        {
//            return new SortedList<Tkey, TValue>(source);
//        }
//    }
//}
