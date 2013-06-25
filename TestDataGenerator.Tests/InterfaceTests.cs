namespace TestDataGenerator.Tests
{
    using FluentAssertions;
    using Xunit;


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

        [Fact]
        public void Catalog_Can_Populate_Interface()
        {
            Catalog catalog = new Catalog();

            catalog.RegisterBuilder<TestInterface>()
                .WithConstructor(() => new TestClass());

            object instance = catalog.CreateInstance(typeof(TestInterface));

            instance.Should().NotBeNull();
            instance.Should().BeAssignableTo<TestInterface>();
        }

        [Fact]
        public void Catalog_Can_Create_Class_With_Interface_Property()
        {
            Catalog catalog = new Catalog();

            catalog.RegisterBuilder<TestInterface>()
                .WithConstructor(() => new TestClass());

            object instance = catalog.CreateInstance(typeof(ClassWithInterfaceProperty));
            instance.Should().BeOfType<ClassWithInterfaceProperty>();

            ClassWithInterfaceProperty sample = instance as ClassWithInterfaceProperty;
            sample.Property.Should().NotBeNull();
            sample.Property.Should().BeAssignableTo<TestInterface>();
        }

        [Fact]
        public void Catalog_Can_Create_Class_With_Interface_In_Constructor()
        {
            Catalog catalog = new Catalog();

            catalog.RegisterBuilder<TestInterface>()
                .WithConstructor(() => new TestClass());

            object instance = catalog.CreateInstance(typeof(ClassWithInterfaceInConstructor));

            instance.Should().BeOfType<ClassWithInterfaceInConstructor>();

            Assert.IsType<ClassWithInterfaceInConstructor>(instance);
            ClassWithInterfaceInConstructor sample = instance as ClassWithInterfaceInConstructor;
            var property = sample.Get();

            property.Should().NotBeNull();
            property.Should().BeAssignableTo<TestInterface>();
        }
    }
}
