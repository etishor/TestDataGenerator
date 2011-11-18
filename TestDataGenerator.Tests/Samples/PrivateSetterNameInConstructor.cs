using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class PrivateSetterNameInConstructor : IAssertEquality
    {
       
       
        public int IntValue { get; private set; }
                
        public PrivateSetterNameInConstructor(int intValue)
        {
            this.IntValue = intValue;           
        }

        public static PrivateSetterNameInConstructor CreateInstance()
        {
            return new PrivateSetterNameInConstructor(10);
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PrivateSetterNameInConstructor>(other);

            PrivateSetterNameInConstructor target = other as PrivateSetterNameInConstructor;

            Assert.AreEqual(this.IntValue, target.IntValue);            
        }
    }
}
