using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class ArrayProperty : IAssertEquality
    {


        public int[] Value { get; set; }

        public static ArrayProperty CreateInstance()
        {
            return new ArrayProperty { Value = new int[] { 10, 20 } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<ArrayProperty>();
            ArrayProperty target = other as ArrayProperty;

            target.Value.Should().Equal(this.Value);
        }
    }
}
