using System;
using MbUnit.Framework;

namespace TestDataGenerator.Tests
{
	[TestFixture]
	public class PrimitiveTests
	{
		enum test
		{
			a, b, c
		}

		private void Verify<T>()
		{
			Catalog catalog = new Catalog();
			Assert.IsInstanceOfType<T>(catalog.CreateInstance(typeof(T)));
		}

		private void VerifyBuilder<T>()
		{
			PrimitieveBuilder builder = new PrimitieveBuilder();

			Assert.IsTrue(builder.CanCreate(typeof(T)));
			Assert.IsInstanceOfType<T>(builder.CreateInstance(typeof(T), null));
		}
		

		[Test]
		public void Catalog_Can_Create_Primitives()
		{
            Verify<byte>();
            Verify<sbyte>();
            Verify<short>();
            Verify<ushort>();
			Verify<int>();
			Verify<uint>();
			Verify<long>();
			Verify<ulong>();
            Verify<float>();
            Verify<double>();
            Verify<decimal>();
            Verify<bool>();			
			Verify<char>();
			Verify<string>();
			Verify<DateTime>();
			Verify<Uri>();
            Verify<Guid>();
		}

		[Test]
		public void Catalog_Can_Create_Enum()
		{
			Verify<test>();
		}

		[Test]
		public void Builder_Can_Create_Primitives()
		{
            VerifyBuilder<byte>();
            VerifyBuilder<sbyte>();
            VerifyBuilder<short>();
            VerifyBuilder<ushort>();
            VerifyBuilder<int>();
            VerifyBuilder<uint>();
            VerifyBuilder<long>();
            VerifyBuilder<ulong>();
            VerifyBuilder<float>();
            VerifyBuilder<double>();
            VerifyBuilder<decimal>();
            VerifyBuilder<bool>();
            VerifyBuilder<char>();
            VerifyBuilder<string>();
            VerifyBuilder<DateTime>();
            VerifyBuilder<Uri>();
            VerifyBuilder<Guid>();			
		}

		[Test]
		public void Builder_Can_Create_Enum()
		{
			VerifyBuilder<test>();
		}

		[Test]
		public void Builder_Creates_String_From_Name()
		{
			PrimitieveBuilder builder = new PrimitieveBuilder();
			object instance = builder.CreateInstance(typeof(string), "test");
			Assert.IsInstanceOfType<string>(instance);
			string stringInstance = instance as string;
			Assert.Contains(stringInstance, "test");
		}

		[Test]
		public void Builder_Creates_Uri_From_Name()
		{
			PrimitieveBuilder builder = new PrimitieveBuilder();
			object instance = builder.CreateInstance(typeof(Uri), "test");
			Assert.IsInstanceOfType<Uri>(instance);
			Uri uri = instance as Uri;
			Assert.Contains(uri.Host, "test");
		}

	}
}
