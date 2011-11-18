using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using MbUnit.Framework;

namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class CustomObject
    {
       
       
        public int Value { get; set; }
    }

    
    
    
    public class CustomObjectProperty : IAssertEquality
    {
       
       
        private CustomObject Value { get; set; }

        public static CustomObjectProperty CreateInstance()
        {
            return new CustomObjectProperty { Value = new CustomObject { Value = 10 } };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<CustomObjectProperty>(other);
            CustomObjectProperty target = other as CustomObjectProperty;

            Assert.AreNotSame(this.Value, target.Value);
            Assert.IsNotNull(this.Value);
            Assert.IsNotNull(target.Value);
            Assert.AreEqual(this.Value.Value, target.Value.Value);
        }
    }
}
