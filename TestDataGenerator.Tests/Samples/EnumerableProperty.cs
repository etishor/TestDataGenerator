using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using MbUnit.Framework;

namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class EnumerableProperty : IAssertEquality
    {
       
       
        private IEnumerable<int> Value { get; set; }

        public static EnumerableProperty CreateInstance()
        {
            return new EnumerableProperty { Value = new int[] { 10, 20 } };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<EnumerableProperty>(other);
            EnumerableProperty target = other as EnumerableProperty;

            Assert.AreElementsEqual(this.Value, target.Value);
        }
    }
}
