using System.Collections.Generic;

using MbUnit.Framework;

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
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<CollectionOfCustomObjects>(other);
            CollectionOfCustomObjects target = other as CollectionOfCustomObjects;

            Assert.AreEqual(this.Count, target.Count);
            Assert.AreElementsEqual(this, target, new Gallio.Common.EqualityComparison<CustomObject>((a, b) => a.Value == b.Value));
        }
    }
}
