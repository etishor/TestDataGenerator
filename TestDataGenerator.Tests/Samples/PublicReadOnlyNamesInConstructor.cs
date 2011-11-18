using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class PublicReadOnlyNamesInConstructor : IAssertEquality
    {
       
       
        public readonly int IntValue;

        public PublicReadOnlyNamesInConstructor(int intValue)
        {
            this.IntValue = intValue;
        }

        public static PublicReadOnlyNamesInConstructor CreateInstance()
        {
            return new PublicReadOnlyNamesInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PublicReadOnlyNamesInConstructor>(other);

            PublicReadOnlyNamesInConstructor target = other as PublicReadOnlyNamesInConstructor;

            Assert.AreEqual(this.IntValue, target.IntValue);            
        }
    }
}
