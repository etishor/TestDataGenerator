using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor : IAssertEquality
    {


        public readonly int IntValue;

        private PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor() { }

        public PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor(int intValue)
        {
            this.IntValue = intValue;
        }

        public static PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor CreateInstance()
        {
            return new PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor>();

            PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor target = other as PublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
