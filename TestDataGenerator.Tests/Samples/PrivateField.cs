using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class PrivateField : IAssertEquality
    {
       
       
        private int Value;

        public static PrivateField CreateInstance()
        {
            return new PrivateField { Value = 10 };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PrivateField>(other);
            PrivateField target = other as PrivateField;

            Assert.AreEqual(this.Value, target.Value);
        }
    }
}
