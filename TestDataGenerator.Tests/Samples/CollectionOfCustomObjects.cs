using System.Collections.Generic;

using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class CollectionOfCustomObjects : List<CustomObject>, IAssertEquality
    {
        public static CollectionOfCustomObjects CreateInstance()
        {
            return new CollectionOfCustomObjects { new CustomObject { Value = 10 } };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<CollectionOfCustomObjects>();
            CollectionOfCustomObjects target = other as CollectionOfCustomObjects;

            target.Count.Should().Be(this.Count);
            target.Should().Equal(this);
        }
    }
}
