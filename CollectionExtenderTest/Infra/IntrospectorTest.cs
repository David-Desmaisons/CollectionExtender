using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NSubstitute;
using CollectionExtender.Infra;

namespace CollectionExtenderTest.Infra
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
