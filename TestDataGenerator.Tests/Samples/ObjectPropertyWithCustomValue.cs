﻿using MbUnit.Framework;

namespace TestDataGenerator.Tests.Samples
{
    public class ObjectPropertyWithCustomValue : IAssertEquality
    {
        public object Value { get; set; }

        public static ObjectPropertyWithCustomValue CreateInstance()
        {
            return new ObjectPropertyWithCustomValue { Value = new CustomObject() { Value = 10 } };
        }

        public void AssertEquality(object other)
        {
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<ObjectPropertyWithCustomValue>(other);
            ObjectPropertyWithCustomValue target = other as ObjectPropertyWithCustomValue;

            CustomObject val = target.Value as CustomObject;
            Assert.IsNotNull(val);
            Assert.AreEqual((this.Value as CustomObject).Value, val.Value);
        }
    }
}
