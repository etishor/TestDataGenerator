using System.Runtime.Serialization;
using Xunit;
using FluentAssertions;


namespace TestDataGenerator.Tests.Samples
{
    [DataContract]
    public class PublicReadOnlyNamesNotInConstructor : IAssertEquality
    {
        [DataMember]
        public readonly int IntValue;

        public PublicReadOnlyNamesNotInConstructor(int otherIntValue)
        {
            this.IntValue = otherIntValue;
        }

        public static PublicReadOnlyNamesNotInConstructor CreateInstance()
        {
            return new PublicReadOnlyNamesNotInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicReadOnlyNamesNotInConstructor>();

            PublicReadOnlyNamesNotInConstructor target = other as PublicReadOnlyNamesNotInConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
