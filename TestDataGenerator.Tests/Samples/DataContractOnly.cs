using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Runtime.Serialization;

namespace TestDataGenerator.Tests.Samples
{
    
    public class DataContractOnly : IAssertEquality
    {
       
        public int ValueField;

       
        public int ValueProperty { get; set; }

        public static DataContractOnly CreateInstance()
        {
            return new DataContractOnly { ValueField = 10, ValueProperty = 20 };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<DataContractOnly>(other);
            DataContractOnly target = other as DataContractOnly;

            Assert.AreEqual(this.ValueField, target.ValueField);
            Assert.AreEqual(this.ValueProperty, target.ValueProperty);
        }
    }
}
