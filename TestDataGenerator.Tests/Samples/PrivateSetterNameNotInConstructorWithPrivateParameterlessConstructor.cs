using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor : IAssertEquality
    {


        public int IntValue { get; private set; }

        private PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor() { }

        public PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor(int otherIntValue)
        {
            this.IntValue = otherIntValue;
        }

        public static PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor CreateInstance()
        {
            return new PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor>();

            PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor target = other as PrivateSetterNameNotInConstructorWithPrivateParameterlessConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
