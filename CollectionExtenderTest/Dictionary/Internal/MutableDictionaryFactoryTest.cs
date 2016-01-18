using MoreCollection.Dictionary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace MoreCollectionTest.Dictionary.Internal
{
    public class MutableDictionaryFactoryTest
    {
        [Fact]
        public void GetDefault_Return_MutableDictionaryFactory()
        {
            var res = MutableDictionaryFactory<string, string>.GetDefault();
            res.Should().BeOfType<MutableSingleDictionary<string, string>>();
        }

        [Fact]
        public void GetDefault_Return_MutableDictionaryFactory_With_Target_MutableSortedDictionary_ComparableT()
        {
            var res = MutableDictionaryFactory<string, string>.GetDefault() 
                as MutableSingleDictionary<string, string>;
            res.TargetType.Should().Be(typeof(MutableSortedDictionary<string, string>));
        }

        [Fact]
        public void GetDefault_Return_MutableDictionaryFactory_With_Target_MutableSortedDictionary_NoneComparableT()
        {
            var res = MutableDictionaryFactory<object, string>.GetDefault()
                as MutableSingleDictionary<object, string>;
            res.TargetType.Should().Be(typeof(MutableListDictionary<object, string>));
        }
    }
}
