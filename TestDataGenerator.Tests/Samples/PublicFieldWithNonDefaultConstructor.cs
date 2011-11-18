using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class PublicFieldWithNonDefaultConstructor : IAssertEquality
    {
       
       
        public int Value;

        public PublicFieldWithNonDefaultConstructor(string name)
        { 
        }

        public static PublicFieldWithNonDefaultConstructor CreateInstance()
        {
            return new PublicFieldWithNonDefaultConstructor("a") { Value = 10 };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PublicFieldWithNonDefaultConstructor>(other);
            PublicFieldWithNonDefaultConstructor target = other as PublicFieldWithNonDefaultConstructor;

            Assert.AreEqual(this.Value, target.Value);
        }
    }
}
