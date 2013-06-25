using System.Runtime.Serialization;
using Xunit;
using FluentAssertions;


namespace TestDataGenerator.Tests.Samples
{
    [DataContract]
    public class PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor : IAssertEquality
    {
        [DataMember]
        public readonly int IntValue;

        private PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor()
        { }

        public PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor(int otherIntValue)
        {
            this.IntValue = otherIntValue;
        }

        public static PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor CreateInstance()
        {
            return new PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor(10);
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor>();

            PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor target = other as PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor;

            target.IntValue.Should().Be(this.IntValue);
        }
    }
}
