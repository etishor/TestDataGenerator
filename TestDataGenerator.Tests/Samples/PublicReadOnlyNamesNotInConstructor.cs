using System.Runtime.Serialization;
using MbUnit.Framework;


namespace TestDataGenerator.Tests.Samples
{
    [DataContract]
    public class PublicReadOnlyNamesNotInConstructor : IAssertEquality
    {
        [DataMember]
        public readonly int IntValue;

        public PublicReadOnlyNamesNotInConstructor(int otherIntValue)
        {
            this.IntValue = otherIntValue;
        }

        public static PublicReadOnlyNamesNotInConstructor CreateInstance()
        {
            return new PublicReadOnlyNamesNotInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PublicReadOnlyNamesNotInConstructor>(other);

            PublicReadOnlyNamesNotInConstructor target = other as PublicReadOnlyNamesNotInConstructor;

            Assert.AreEqual(this.IntValue, target.IntValue);
        }
    }
}
