using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{
    public class NoDataContractOnly : IAssertEquality
    {
        public int ValueField;

        public int ValueProperty { get; set; }

        public static NoDataContractOnly CreateInstance()
        {
            return new NoDataContractOnly { ValueField = 10, ValueProperty = 20 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<NoDataContractOnly>();
            NoDataContractOnly target = other as NoDataContractOnly;

            target.ValueField.Should().Be(this.ValueField);
            target.ValueProperty.Should().Be(this.ValueProperty);
        }
    }
}
