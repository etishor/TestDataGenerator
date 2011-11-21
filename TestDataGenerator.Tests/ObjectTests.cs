using System;
using MbUnit.Framework;
using TestDataGenerator.Tests.Samples;

namespace TestDataGenerator.Tests
{
    [TestFixture]
    public class ObjectTests
    {
        class Sample
        {
            private readonly int intValue;
            private readonly string stringValue;

            private Sample()
            {
                this.intValue = -1;
                this.stringValue = null;
                this.StringProp = null;
                this.DateProp = DateTime.MinValue;
            }

            public Sample(int intVal, string stringVal)
                : this()
            {
                this.intValue = intVal;
                this.stringValue = stringVal;
            }

            public int PrivateInt { get { return intValue; } }
            public string PrivateString { get { return stringValue; } }

            public string StringProp { get; set; }
            public DateTime DateProp { get; set; }
        }

        [Test]
        public void Catalog_Can_Create_Using_Consutrctor()
        {
            Catalog catalog = new Catalog();
            object instance = catalog.CreateInstance(typeof(Sample));

            Assert.IsInstanceOfType(typeof(Sample), instance);
            Sample sample = instance as Sample;

            Assert.AreNotEqual(-1, sample.PrivateInt);
            Assert.IsNotNull(sample.PrivateString);
            Assert.IsNotNull(sample.StringProp);
            Assert.AreNotEqual(DateTime.MinValue, sample.DateProp);
        }

        class TestClass
        {
            public readonly int Value = -1;

            public TestClass(int value)
            {
                this.Value = value;
            }
        }

        [Test]
        public void Catalog_Can_Create_Object_With_Public_Readonly_Field()
        {
            Catalog catalog = new Catalog();
            object instance = catalog.CreateInstance<TestClass>();
            Assert.IsInstanceOfType<TestClass>(instance);
            TestClass cl = instance as TestClass;
            Assert.AreNotEqual(-1, cl.Value);
        }
    }
}
