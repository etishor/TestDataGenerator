
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{
    public class CustomObject
    {
        public int Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as CustomObject;
            if (other == null)
            {
                return false;
            }

            return this.Value == other.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }

    public class CustomObjectProperty : IAssertEquality
    {
        public CustomObject Value { get; set; }

        public static CustomObjectProperty CreateInstance()
        {
            return new CustomObjectProperty { Value = new CustomObject { Value = 10 } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<CustomObjectProperty>();
            CustomObjectProperty target = other as CustomObjectProperty;

            target.Value.Should().NotBeSameAs(this.Value);
            this.Value.Should().NotBeNull();
            target.Value.Should().NotBeNull();
            target.Value.Value.Should().Be(this.Value.Value);
        }
    }
}
