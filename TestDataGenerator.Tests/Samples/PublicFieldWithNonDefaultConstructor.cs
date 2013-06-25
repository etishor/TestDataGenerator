using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PublicFieldWithNonDefaultConstructor : IAssertEquality
    {


        public int Value;

        public PublicFieldWithNonDefaultConstructor(string name)
        {
        }

        public static PublicFieldWithNonDefaultConstructor CreateInstance()
        {
            return new PublicFieldWithNonDefaultConstructor("a") { Value = 10 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicFieldWithNonDefaultConstructor>();
            PublicFieldWithNonDefaultConstructor target = other as PublicFieldWithNonDefaultConstructor;

            target.Value.Should().Be(this.Value);
        }
    }
}
