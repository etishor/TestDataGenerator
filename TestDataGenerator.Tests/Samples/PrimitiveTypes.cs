using System;

using MbUnit.Framework;

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
            Assert.IsNotNull(other);
            Assert.IsInstanceOfType<PrimitiveTypes>(other);

            PrimitiveTypes target = other as PrimitiveTypes;

            Assert.AreEqual(this.IntValue, target.IntValue);
            Assert.AreEqual(this.DecimalValue, target.DecimalValue);
            Assert.AreApproximatelyEqual(this.DateValue, target.DateValue, TimeSpan.FromMilliseconds(1));
            Assert.AreEqual(this.TimeSpanValue, target.TimeSpanValue);
            Assert.AreEqual(this.GuidValue, target.GuidValue);
            Assert.AreEqual(this.StringValue, target.StringValue);
            Assert.AreEqual(this.LongValue, target.LongValue);
            Assert.AreEqual(Math.Round(this.DoubleValue, 6), Math.Round(target.DoubleValue, 6));
        }

       
    }
}
