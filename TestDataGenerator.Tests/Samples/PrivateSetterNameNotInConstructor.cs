using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PrivateSetterNameNotInConstructor : IAssertEquality
    {


        public int IntValue { get; private set; }

        public PrivateSetterNameNotInConstructor(int otherIntValue)
        {
            this.IntValue = otherIntValue;
        }

        public static PrivateSetterNameNotInConstructor CreateInstance()
        {
            return new PrivateSetterNameNotInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PrivateSetterNameNotInConstructor>();

            PrivateSetterNameNotInConstructor target = other as PrivateSetterNameNotInConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
