using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class EnumerableProperty : IAssertEquality
    {


        private IEnumerable<int> Value { get; set; }

        public static EnumerableProperty CreateInstance()
        {
            return new EnumerableProperty { Value = new int[] { 10, 20 } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<EnumerableProperty>();
            EnumerableProperty target = other as EnumerableProperty;

            Assert.Equal(target.Value, this.Value);
        }
    }
}
