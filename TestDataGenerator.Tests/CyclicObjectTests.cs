using MbUnit.Framework;

namespace TestDataGenerator.Tests
{
	[TestFixture]
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
				Assert.IsNull(sample.SampleRef);
				return;
			}
			
			Assert.IsNotNull(sample.SampleRef);
			Veirfy(sample.SampleRef, level + 1, maxLevel);
		}

		[Test]
		public void Catalog_Can_Create_Cyclic_Object()
		{
			Catalog catalog = new Catalog();
			object instance = catalog.CreateInstance<Sample>();

			Assert.IsInstanceOfType<Sample>(instance);
			Sample sample = instance as Sample;

			Veirfy(sample, 0, catalog.MaxRecursionDepth);
		}

		[Test]
		public void Catalog_Can_Create_Array_Of_Cyclic_Object()
		{
			Catalog catalog = new Catalog();
			object instance = catalog.CreateInstance<Sample[]>();

			Assert.IsInstanceOfType<Sample[]>(instance);
			Sample[] sample = instance as Sample[];

			foreach (Sample s in sample)
			{
				Veirfy(s, 1, catalog.MaxRecursionDepth);
			}
		}
	}
}
