using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public sealed class SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor : IAssertEquality
    {


        public readonly int IntValue;

        private SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor() { }

        public SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor(int intValue)
        {
            this.IntValue = intValue;
        }

        public static SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor CreateInstance()
        {
            return new SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor>();

            SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor target = other as SealedPublicReadOnlyNamesInConstructorWithPrivateParameterlessConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
