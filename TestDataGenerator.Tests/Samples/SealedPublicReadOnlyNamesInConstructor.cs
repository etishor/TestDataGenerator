using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public sealed class SealedPublicReadOnlyNamesInConstructor : IAssertEquality
    {


        public readonly int IntValue;

        public SealedPublicReadOnlyNamesInConstructor(int intValue)
        {
            this.IntValue = intValue;
        }

        public static SealedPublicReadOnlyNamesInConstructor CreateInstance()
        {
            return new SealedPublicReadOnlyNamesInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<SealedPublicReadOnlyNamesInConstructor>();

            SealedPublicReadOnlyNamesInConstructor target = other as SealedPublicReadOnlyNamesInConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
