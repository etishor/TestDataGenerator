using System.Runtime.Serialization;
using MbUnit.Framework;


namespace TestDataGenerator.Tests.Samples
{
    [DataContract]
    public class PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor : IAssertEquality
    {
        [DataMember]
        public readonly int IntValue;

        private PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor()
        { }

        public PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor(int otherIntValue)
        {
            this.IntValue = otherIntValue;
        }

        public static PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor CreateInstance()
        {
            return new PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor(10);
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor>(other);

            PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor target = other as PublicReadOnlyNamesNotInConstructorWithPrivateParameterlessConstructor;

            Assert.AreEqual(this.IntValue, target.IntValue);
        }
    }
}
