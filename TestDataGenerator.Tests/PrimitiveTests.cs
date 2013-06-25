using System;
using FluentAssertions;
using Xunit;

namespace TestDataGenerator.Tests
{

    public class PrimitiveTests
    {
        enum test
        {
            a, b, c
        }

        private void Verify<T>()
        {
            Catalog catalog = new Catalog();
            catalog.CreateInstance(typeof(T))
                .Should().BeOfType<T>();
        }

        private void VerifyBuilder<T>()
        {
            PrimitieveBuilder builder = new PrimitieveBuilder();

            builder.CanCreate(typeof(T))
                .Should().BeTrue();

            builder.CreateInstance(typeof(T), null)
                .Should().BeOfType<T>();
        }


        [Fact]
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
            Verify<TimeSpan>();
            Verify<DateTime>();
            Verify<Uri>();
            Verify<Guid>();
        }

        [Fact]
        public void Catalog_Can_Create_Enum()
        {
            Verify<test>();
        }

        [Fact]
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
            VerifyBuilder<TimeSpan>();
            VerifyBuilder<DateTime>();
            VerifyBuilder<Uri>();
            VerifyBuilder<Guid>();
        }

        [Fact]
        public void Builder_Can_Create_Enum()
        {
            VerifyBuilder<test>();
        }

        [Fact]
        public void Builder_Creates_String_From_Name()
        {
            PrimitieveBuilder builder = new PrimitieveBuilder();
            object instance = builder.CreateInstance(typeof(string), "test");
            instance.Should().BeOfType<string>();
            string stringInstance = instance as string;
            stringInstance.Should().Contain("test");
        }

        [Fact]
        public void Builder_Creates_Uri_From_Name()
        {
            PrimitieveBuilder builder = new PrimitieveBuilder();
            object instance = builder.CreateInstance(typeof(Uri), "test");

            instance.Should().BeOfType<Uri>();
            Uri uri = instance as Uri;
            uri.Host.Should().Contain("test");
        }

    }
}
