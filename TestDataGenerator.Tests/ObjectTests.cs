using System;
using MbUnit.Framework;

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
				:this()
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

	}
}
