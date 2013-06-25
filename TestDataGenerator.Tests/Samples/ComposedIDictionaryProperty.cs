using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class ComposedIDictionaryProperty : IAssertEquality
    {


        public IDictionary<int, List<string>> Value { get; set; }

        public static ComposedIDictionaryProperty CreateInstance()
        {
            return new ComposedIDictionaryProperty { Value = new Dictionary<int, List<string>> { { 10, new List<string> { "a" } } } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<ComposedIDictionaryProperty>();
            ComposedIDictionaryProperty target = other as ComposedIDictionaryProperty;
            target.Value.Should().NotBeNull();
            target.Value.Count.Should().Be(this.Value.Count);
            foreach (var kvp in this.Value)
            {
                var first = kvp.Value;
                var second = target.Value[kvp.Key];
                second.Should().Equal(first);
            }
        }
    }
}
