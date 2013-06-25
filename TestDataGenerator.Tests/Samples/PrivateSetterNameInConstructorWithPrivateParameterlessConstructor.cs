using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PrivateSetterNameInConstructorWithPrivateParameterlessConstructor : IAssertEquality
    {


        public int IntValue { get; private set; }

        private PrivateSetterNameInConstructorWithPrivateParameterlessConstructor() { }

        public PrivateSetterNameInConstructorWithPrivateParameterlessConstructor(int intValue)
        {
            this.IntValue = intValue;
        }

        public static PrivateSetterNameInConstructorWithPrivateParameterlessConstructor CreateInstance()
        {
            return new PrivateSetterNameInConstructorWithPrivateParameterlessConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PrivateSetterNameInConstructorWithPrivateParameterlessConstructor>();

            PrivateSetterNameInConstructorWithPrivateParameterlessConstructor target = other as PrivateSetterNameInConstructorWithPrivateParameterlessConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
