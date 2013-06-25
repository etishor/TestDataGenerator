using System;

using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{



    public class PrimitiveTypes : IAssertEquality
    {


        public int IntValue;



        public decimal DecimalValue;



        public DateTime DateValue;



        public TimeSpan TimeSpanValue;



        public Guid GuidValue;


        public string StringValue;


        public long LongValue;


        public double DoubleValue;

        public static PrimitiveTypes CreateInstance()
        {
            return new PrimitiveTypes
            {
                IntValue = 10,
                DecimalValue = 10.1m,
                DateValue = DateTime.Now,
                TimeSpanValue = TimeSpan.FromSeconds(20),
                GuidValue = Guid.NewGuid(),
                StringValue = "string",
                LongValue = long.MaxValue,
                DoubleValue = 1654651.13165D
            };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PrimitiveTypes>();

            PrimitiveTypes target = other as PrimitiveTypes;

            target.IntValue.Should().Be(this.IntValue);
            target.DecimalValue.Should().Be(this.DecimalValue);
            target.DateValue.Should().BeCloseTo(this.DateValue);
            target.TimeSpanValue.Should().Be(this.TimeSpanValue);
            target.GuidValue.Should().Be(this.GuidValue);
            target.StringValue.Should().Be(this.StringValue);
            target.LongValue.Should().Be(this.LongValue);
            target.DoubleValue.Should().BeApproximately(this.DoubleValue, 0.01);
        }


    }
}
