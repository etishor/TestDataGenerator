// -----------------------------------------------------------------------
// <copyright file="InterfaceTests.cs" company="Recognos Romania">
// {RecognosCopyrightTextPlaceholder}
// </copyright>
// -----------------------------------------------------------------------

namespace TestDataGenerator.Tests
{
    using MbUnit.Framework;

    [TestFixture]
    public class InterfaceTests
    {
        interface TestInterface
        {
            int Key { get; }
        }

        class TestClass : TestInterface
        {
            public int Key { get; set; }
        }

        class ClassWithInterfaceProperty
        {
            public TestInterface Property { get; set; }
        }

        class ClassWithInterfaceInConstructor
        {
            private readonly TestInterface member;

            public ClassWithInterfaceInConstructor(TestInterface member)
            {
                this.member = member;
            }

            public TestInterface Get()
            {
                return this.member;
            }
        }

        [Test]
        public void Catalog_Can_Populate_Interface()
        {
            Catalog catalog = new Catalog();

            catalog.RegisterBuilder<TestInterface>()
                .WithConstructor(() => new TestClass());

            object instance = catalog.CreateInstance(typeof(TestInterface));

            Assert.IsNotNull(instance, "instance");
            Assert.IsInstanceOfType<TestInterface>(instance);
        }

        [Test]
        public void Catalog_Can_Create_Class_With_Interface_Property()
        {
            Catalog catalog = new Catalog();

            catalog.RegisterBuilder<TestInterface>()
                .WithConstructor(() => new TestClass());

            object instance = catalog.CreateInstance(typeof(ClassWithInterfaceProperty));
            Assert.IsInstanceOfType<ClassWithInterfaceProperty>(instance);
            ClassWithInterfaceProperty sample = instance as ClassWithInterfaceProperty;
            Assert.IsInstanceOfType<TestInterface>(sample.Property);
        }

        [Test]
        public void Catalog_Can_Create_Class_With_Interface_In_Constructor()
        {
            Catalog catalog = new Catalog();

            catalog.RegisterBuilder<TestInterface>()
                .WithConstructor(() => new TestClass());

            object instance = catalog.CreateInstance(typeof(ClassWithInterfaceInConstructor));
            Assert.IsInstanceOfType<ClassWithInterfaceInConstructor>(instance);
            ClassWithInterfaceInConstructor sample = instance as ClassWithInterfaceInConstructor;
            Assert.IsInstanceOfType<TestInterface>(sample.Get());
        }
    }
}
