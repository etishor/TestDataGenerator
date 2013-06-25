using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PrivateSetterNameInConstructor : IAssertEquality
    {


        public int IntValue { get; private set; }

        public PrivateSetterNameInConstructor(int intValue)
        {
            this.IntValue = intValue;
        }

        public static PrivateSetterNameInConstructor CreateInstance()
        {
            return new PrivateSetterNameInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PrivateSetterNameInConstructor>();

            PrivateSetterNameInConstructor target = other as PrivateSetterNameInConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
