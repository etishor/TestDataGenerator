
namespace TestDataGenerator.Tests.Samples
{
    using System;
    using System.Runtime.Serialization;
    using Xunit;
    using FluentAssertions;

    public class UriWithQuotesAndSlashes : IAssertEquality
    {
        public const string UriQuotes = @"http://test.com/%22foo+bar%22";
        public const string UriSlash = @"http://tes/?a=b\\c&d=e\";

        public Uri ValueWithQuotes { get; set; }

        public Uri ValueWithSlashes { get; set; }

        public static UriWithQuotesAndSlashes CreateInstance()
        {
            return new UriWithQuotesAndSlashes
            {
                ValueWithQuotes = new Uri(UriQuotes),
                ValueWithSlashes = new Uri(UriSlash)
            };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<UriWithQuotesAndSlashes>();
            UriWithQuotesAndSlashes target = other as UriWithQuotesAndSlashes;

            target.ValueWithQuotes.Should().Be(this.ValueWithQuotes);
            target.ValueWithSlashes.Should().Be(this.ValueWithSlashes);
        }
    }
}
