using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{
    public class ArrayField : IAssertEquality
    {
        private int[] Value;

        public static ArrayField CreateInstance()
        {
            return new ArrayField { Value = new int[] { 10, 20 } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<ArrayField>();
            ArrayField target = other as ArrayField;

            target.Value.Should().Equal(this.Value);
        }
    }
}
