﻿using System;

namespace MoreCollection.Set.Infra
{
    internal class LetterSimpleSetFactoryBuilder<T>  where T : class
    {
        static LetterSimpleSetFactoryBuilder()
        {
            GetFactory = (maxList) => new LetterSimpleSetFactory<T>(maxList);
        }

        internal static Func<int, ILetterSimpleSetFactory<T>> GetFactory
        {
            get; set;
        }
    }
}
