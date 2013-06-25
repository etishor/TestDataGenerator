using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{
    public class ObjectPropertyWithCustomValue : IAssertEquality
    {
        public object Value { get; set; }

        public static ObjectPropertyWithCustomValue CreateInstance()
        {
            return new ObjectPropertyWithCustomValue { Value = new CustomObject() { Value = 10 } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<ObjectPropertyWithCustomValue>();
            ObjectPropertyWithCustomValue target = other as ObjectPropertyWithCustomValue;

            CustomObject val = target.Value as CustomObject;

            val.Should().NotBeNull();
            val.Value.Should().Be((this.Value as CustomObject).Value);
        }
    }
}
