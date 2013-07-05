
using FluentAssertions;
using Xunit;

namespace TestDataGenerator.Tests.Samples
{
    public class JaggedArray : IAssertEquality
    {
        public int[][] Value { get; set; }

        public static JaggedArray CreateInstance()
        {
            return new JaggedArray { Value = new int[][] { new int[] { 10, 20 }, new int[] { 30, 40 } } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<JaggedArray>();
            JaggedArray target = other as JaggedArray;

            Assert.Equal(target.Value, this.Value);
        }
    }
}
