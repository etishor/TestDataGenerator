using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Xunit;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class CollectionOfStrings : List<string>, IAssertEquality
    {
        public static CollectionOfStrings CreateInstance()
        {
            return new CollectionOfStrings { "test" };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<CollectionOfStrings>();
            CollectionOfStrings target = other as CollectionOfStrings;
            target.Should().Equal(this);
        }
    }
}
