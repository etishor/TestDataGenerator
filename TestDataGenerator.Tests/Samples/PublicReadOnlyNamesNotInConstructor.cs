using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class PublicReadOnlyNamesNotInConstructor : IAssertEquality
    {
       
       
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
