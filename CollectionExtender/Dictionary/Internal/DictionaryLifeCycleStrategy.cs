//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CollectionExtender.Dictionary.Internal
//{
//    internal abstract class DictionaryLifeCycleStrategy<Tkey, TValue> : IDictionaryLifeCycleStrategy<Tkey, TValue>
//    {
//        private readonly int _TransitionToDictionary;
//        internal DictionaryLifeCycleStrategy(int transitionToDictionary = 25)
//        {
//            _TransitionToDictionary = transitionToDictionary;
//        }

//        protected abstract IDictionary<Tkey, TValue> BuildListBasedDictionary(IDictionary<Tkey, TValue> source);

//        public void Add(ref IDictionary<Tkey, TValue> dictionary, Tkey key, TValue value)
//        {
//            if (key == null)
//                throw new ArgumentNullException("key");

//            if (dictionary == null)
//            {
//                dictionary = new SingleDictionary<Tkey, TValue>(key, value);
//                return;
//            }
            
//            if (dictionary.Count == 1)
//            {
//                dictionary = BuildListBasedDictionary(dictionary);
//            }
//            else if (dictionary.Count == _TransitionToDictionary)
//            {
//                dictionary = new Dictionary<Tkey, TValue>(dictionary);
//            }

//            dictionary.Add(key, value);
//        }

//        public bool Remove(ref IDictionary<Tkey, TValue> dictionary, Tkey key)
//        {
//            if (key == null)
//                throw new ArgumentNullException("key");

//            if (dictionary == null)
//            {
//                return false;
//            }

//            if (dictionary.Count == 1)
//            {
//                if (!dictionary.ContainsKey(key))
//                    return false;

//                dictionary = null;
//                return true;
//            }

//            if (dictionary.Count == 2)
//            {
//                if (!dictionary.Remove(key))
//                    return false;

//                var keyvaluepair = dictionary.First();
//                dictionary = new SingleDictionary<Tkey, TValue>(keyvaluepair.Key, keyvaluepair.Value);
//                return true;
//            }
            
//            if (dictionary.Count == _TransitionToDictionary + 1)
//            {
//                if (!dictionary.Remove(key))
//                    return false;

//                dictionary = BuildListBasedDictionary(dictionary);
//                return true;
//            }

//            return dictionary.Remove(key);
//        }


//        public void Update(ref IDictionary<Tkey, TValue> dictionary, Tkey key, TValue value)
//        {
//            if (key == null)
//                throw new ArgumentNullException("key");

//            if (dictionary == null)
//            {
//                dictionary = new SingleDictionary<Tkey, TValue>(key, value);
//                return;
//            }

//            if (dictionary.Count == 1)
//            {
//                if (dictionary.ContainsKey(key))
//                {
//                    dictionary[key] = value;
//                    return;
//                }

//                dictionary = BuildListBasedDictionary(dictionary);
//            }

//            if (dictionary.Count == _TransitionToDictionary)
//            {
//                if (dictionary.ContainsKey(key))
//                {
//                    dictionary[key] = value;
//                    return;
//                }

//                dictionary = new Dictionary<Tkey, TValue>(dictionary);
//            }

//            dictionary[key] = value;
//        }
//    }
//}
