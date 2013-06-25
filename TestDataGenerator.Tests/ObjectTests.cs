
using System;
using Xunit;

namespace TestDataGenerator.Tests
{

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

        [Fact]
        public void Catalog_Can_Create_Using_Consutrctor()
        {
            Catalog catalog = new Catalog();
            object instance = catalog.CreateInstance(typeof(Sample));

            Assert.IsType(typeof(Sample), instance);
            Sample sample = instance as Sample;

            Assert.NotEqual(-1, sample.PrivateInt);
            Assert.NotNull(sample.PrivateString);
            Assert.NotNull(sample.StringProp);
            Assert.NotEqual(DateTime.MinValue, sample.DateProp);
        }

        class TestClass
        {
            public readonly int Value = -1;

            public TestClass(int value)
            {
                this.Value = value;
            }
        }

        [Fact]
        public void Catalog_Can_Create_Object_With_Public_Readonly_Field()
        {
            Catalog catalog = new Catalog();
            object instance = catalog.CreateInstance<TestClass>();
            Assert.IsType<TestClass>(instance);
            TestClass cl = instance as TestClass;
            Assert.NotEqual(-1, cl.Value);
        }
    }
}
