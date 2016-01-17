using System;
using Xunit;
using FluentAssertions;
using MoreCollection.Infra;

namespace MoreCollectionTest.Infra
{
    public class IntrospectorTest
    {
        [Fact]
        public void Build_Create_Object_WithoutParameters()
        {
            var res = Introspector.Build<Object>();
            res.Should().NotBeNull();
        }

        [Fact]
        public void Build_Create_Object_WithParameters()
        {
            var res = Introspector.Build<Exception>("Unknown Exception");
            res.Should().NotBeNull();
            res.Message.Should().Be("Unknown Exception");
        }
    }
}
