using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class PrivateSetterNameInConstructorWithPrivateParameterlessConstructor : IAssertEquality
    {
       
       
        public int IntValue { get; private set; }

        private PrivateSetterNameInConstructorWithPrivateParameterlessConstructor() { }

        public PrivateSetterNameInConstructorWithPrivateParameterlessConstructor(int intValue)
        {
            this.IntValue = intValue;           
        }

        public static PrivateSetterNameInConstructorWithPrivateParameterlessConstructor CreateInstance()
        {
            return new PrivateSetterNameInConstructorWithPrivateParameterlessConstructor(10);
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PrivateSetterNameInConstructorWithPrivateParameterlessConstructor>(other);

            PrivateSetterNameInConstructorWithPrivateParameterlessConstructor target = other as PrivateSetterNameInConstructorWithPrivateParameterlessConstructor;

            Assert.AreEqual(this.IntValue, target.IntValue);            
        }
    }
}
