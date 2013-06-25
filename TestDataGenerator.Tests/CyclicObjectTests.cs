using FluentAssertions;
using Xunit;

namespace TestDataGenerator.Tests
{
    public class CyclicObjectTests
    {
        class Sample
        {
            public Sample SampleRef { get; set; }
        }

        void Veirfy(Sample sample, int level, int maxLevel)
        {
            if (level >= maxLevel)
            {
                sample.SampleRef.Should().BeNull();
                return;
            }

            sample.SampleRef.Should().NotBeNull();
            Veirfy(sample.SampleRef, level + 1, maxLevel);
        }

        [Fact]
        public void Catalog_Can_Create_Cyclic_Object()
        {
            Catalog catalog = new Catalog();
            object instance = catalog.CreateInstance<Sample>();

            instance.Should().BeOfType<Sample>();

            Sample sample = instance as Sample;

            Veirfy(sample, 0, catalog.MaxRecursionDepth);
        }

        [Fact]
        public void Catalog_Can_Create_Array_Of_Cyclic_Object()
        {
            Catalog catalog = new Catalog();
            object instance = catalog.CreateInstance<Sample[]>();

            instance.Should().BeOfType<Sample[]>();
            Sample[] sample = instance as Sample[];

            foreach (Sample s in sample)
            {
                Veirfy(s, 1, catalog.MaxRecursionDepth);
            }
        }
    }
}
