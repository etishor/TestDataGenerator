using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PublicReadOnlyNamesInConstructor : IAssertEquality
    {


        public readonly int IntValue;

        public PublicReadOnlyNamesInConstructor(int intValue)
        {
            this.IntValue = intValue;
        }

        public static PublicReadOnlyNamesInConstructor CreateInstance()
        {
            return new PublicReadOnlyNamesInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicReadOnlyNamesInConstructor>();

            PublicReadOnlyNamesInConstructor target = other as PublicReadOnlyNamesInConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
