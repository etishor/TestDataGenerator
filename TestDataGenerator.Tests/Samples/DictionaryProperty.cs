using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class DictionaryProperty : IAssertEquality
    {


        private Dictionary<int, string> Value { get; set; }

        public static DictionaryProperty CreateInstance()
        {
            return new DictionaryProperty { Value = new Dictionary<int, string> { { 10, "a" } } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<DictionaryProperty>();
            DictionaryProperty target = other as DictionaryProperty;

            Assert.Equal(target.Value, this.Value);
        }
    }
}
