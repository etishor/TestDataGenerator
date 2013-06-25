using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PrivateProperty : IAssertEquality
    {


        private int Value { get; set; }

        public static PrivateProperty CreateInstance()
        {
            return new PrivateProperty { Value = 10 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PrivateProperty>();
            PrivateProperty target = other as PrivateProperty;

            target.Value.Should().Be(this.Value);
        }
    }
}
