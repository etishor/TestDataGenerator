using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;

namespace TestDataGenerator.Tests.Samples
{

    public class DataContractOnly : IAssertEquality
    {

        public int ValueField;


        public int ValueProperty { get; set; }

        public static DataContractOnly CreateInstance()
        {
            return new DataContractOnly { ValueField = 10, ValueProperty = 20 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<DataContractOnly>();
            DataContractOnly target = other as DataContractOnly;

            target.ValueField.Should().Be(this.ValueField);
            target.ValueProperty.Should().Be(this.ValueProperty);
        }
    }
}
