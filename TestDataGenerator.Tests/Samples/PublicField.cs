using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PublicField : IAssertEquality
    {


        public int Value;

        public static PublicField CreateInstance()
        {
            return new PublicField { Value = 10 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicField>();
            PublicField target = other as PublicField;

            target.Value.Should().Be(this.Value);
        }
    }
}
