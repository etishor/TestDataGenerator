
using FluentAssertions;
using Xunit;

namespace TestDataGenerator.Tests.Samples
{
    public class MultiDimensionsArray : IAssertEquality
    {
        public int[][] Value { get; set; }

        public static MultiDimensionsArray CreateInstance()
        {
            return new MultiDimensionsArray { Value = new int[][] { new int[] { 10, 20 }, new int[] { 30, 40 } } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<MultiDimensionsArray>();
            MultiDimensionsArray target = other as MultiDimensionsArray;

            Assert.Equal(target.Value, this.Value);
        }
    }
}
