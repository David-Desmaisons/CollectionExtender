﻿using MoreCollection.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NSubstitute;
using MoreCollection.Dictionary.Internal.Strategy;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableSortedDictionaryTest : MutableMiddleDictionaryTest
    {
        private readonly IDictionaryStrategy<string, string> _DictionarySwitcher;

        public MutableSortedDictionaryTest()
        {
            _DictionarySwitcher = Substitute.For<IDictionaryStrategy<string, string>>();
        }

        protected override IMutableDictionary<string, string> Get(IDictionary<string, string> Original,  IDictionaryStrategy<string, string> strategy)
        {
            return new MutableSortedDictionary<string, string>(Original, strategy);
        }

        protected override IMutableDictionary<string, string> GetEmpty()
        {
            return new MutableSortedDictionary<string, string>(new OrderedDictionaryStrategy<string, string>(4));
        }
    }
}
