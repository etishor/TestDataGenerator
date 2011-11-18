using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using MbUnit.Framework;

namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class ListProperty : IAssertEquality
    {
       
       
        private List<int> Value { get; set; }

        public static ListProperty CreateInstance()
        {
            return new ListProperty { Value = new List<int> { 10, 20 } };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<ListProperty>(other);
            ListProperty target = other as ListProperty;

            Assert.AreElementsEqual(this.Value, target.Value);
        }
    }
}
