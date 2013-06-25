using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace TestDataGenerator.Tests.Samples
{



    public class IDictionaryProperty : IAssertEquality
    {


        private IDictionary<int, string> Value { get; set; }

        public static IDictionaryProperty CreateInstance()
        {
            return new IDictionaryProperty { Value = new Dictionary<int, string> { { 10, "a" } } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<IDictionaryProperty>();
            IDictionaryProperty target = other as IDictionaryProperty;

            Assert.Equal(target.Value, this.Value);
        }
    }
}
