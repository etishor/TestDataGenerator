using System.Collections.Generic;

using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class ListProperty : IAssertEquality
    {


        public List<int> Value { get; set; }

        public static ListProperty CreateInstance()
        {
            return new ListProperty { Value = new List<int> { 10, 20 } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<ListProperty>();
            ListProperty target = other as ListProperty;

            target.Value.Should().Equal(this.Value);
        }
    }
}
