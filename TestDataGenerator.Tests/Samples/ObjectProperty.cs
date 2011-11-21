using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using MbUnit.Framework;

namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class ObjectProperty : IAssertEquality
    {
       
       
        public object Value {get;set;}

        public static ObjectProperty CreateInstance()
        {
            return new ObjectProperty { Value = "test" };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<ObjectProperty>(other);
            ObjectProperty target = other as ObjectProperty;
            Assert.IsNotNull(this.Value);
            Assert.IsInstanceOfType(this.Value.GetType(), target.Value);
        }
    }
}
