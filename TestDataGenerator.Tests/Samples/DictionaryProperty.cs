using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using MbUnit.Framework;

namespace TestDataGenerator.Tests.Samples
{
    
    
    
    public class DictionaryProperty : IAssertEquality
    {
       
       
        private Dictionary<int,string> Value { get; set; }

        public static DictionaryProperty CreateInstance()
        {
            return new DictionaryProperty { Value = new Dictionary<int, string> { { 10, "a" } } };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<DictionaryProperty>(other);
            DictionaryProperty target = other as DictionaryProperty;

            Assert.AreElementsEqual(this.Value, target.Value);
        }
    }
}
