using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class UriField : IAssertEquality
    {


        public Uri Value;

        public static UriField CreateInstance()
        {
            return new UriField { Value = new Uri("http://uri/") };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<UriField>();
            UriField target = other as UriField;

            target.Value.Should().Be(this.Value);
        }
    }
}
