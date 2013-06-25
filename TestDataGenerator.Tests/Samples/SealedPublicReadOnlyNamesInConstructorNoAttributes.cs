using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{
    public sealed class SealedPublicReadOnlyNamesInConstructorNoAttributes : IAssertEquality
    {
        public readonly int IntValue;

        public SealedPublicReadOnlyNamesInConstructorNoAttributes(int intValue)
        {
            this.IntValue = intValue;
        }

        public static SealedPublicReadOnlyNamesInConstructorNoAttributes CreateInstance()
        {
            return new SealedPublicReadOnlyNamesInConstructorNoAttributes(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<SealedPublicReadOnlyNamesInConstructorNoAttributes>();

            SealedPublicReadOnlyNamesInConstructorNoAttributes target = other as SealedPublicReadOnlyNamesInConstructorNoAttributes;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
