using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class ObjectProperty : IAssertEquality
    {


        public object Value { get; set; }

        public static ObjectProperty CreateInstance()
        {
            return new ObjectProperty { Value = "test" };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<ObjectProperty>();
            ObjectProperty target = other as ObjectProperty;
            this.Value.Should().NotBeNull();
            target.Value.GetType().Should().Be(this.Value.GetType());
        }
    }
}
