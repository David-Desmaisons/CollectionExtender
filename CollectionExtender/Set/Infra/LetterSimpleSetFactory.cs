﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionExtender.Set.Infra
{
    internal static class LetterSimpleSetFactory<T> where T : class
    {
        public static int MaxList = 10;

        internal static  ILetterSimpleSet<T> GetDefault()
        {
            return new SingleSet<T>();
        }

        internal static  ILetterSimpleSet<T> GetDefault(T Item)
        {
            return new SingleSet<T>(Item);
        }

        internal static  ILetterSimpleSet<T> GetDefault(IEnumerable<T> Items)
        {
            if (Items == null)
                throw new ArgumentNullException("Items");

            var FiItems = new HashSet<T>(Items);

            int count = FiItems.Count;
            if (count >= MaxList)
                return new SimpleHashSet<T>(FiItems);

            if (count > 1)
                return new ListSet<T>(FiItems);

            if (count == 1)
                return GetDefault(FiItems.First());

            return GetDefault();
        }
    }
}
