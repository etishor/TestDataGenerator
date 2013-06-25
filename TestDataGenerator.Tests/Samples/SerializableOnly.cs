using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{

    public class SerializableOnly : IAssertEquality
    {
        public int ValueField;

        public int ValueProperty { get; set; }

        public static SerializableOnly CreateInstance()
        {
            return new SerializableOnly { ValueField = 10, ValueProperty = 20 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<SerializableOnly>();
            SerializableOnly target = other as SerializableOnly;

            target.ValueField.Should().Be(this.ValueField);
            target.ValueProperty.Should().Be(this.ValueProperty);
        }
    }
}
