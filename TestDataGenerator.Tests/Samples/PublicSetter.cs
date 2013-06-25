using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PublicSetter : IAssertEquality
    {


        public int IntValue { get; set; }

        public static PublicSetter CreateInstance()
        {
            return new PublicSetter { IntValue = 10 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicSetter>();

            PublicSetter target = other as PublicSetter;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
