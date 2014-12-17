using System;
using FluentAssertions;

namespace TestDataGenerator.Tests.Samples
{
    public sealed class PublicReadonlyFieldWithNamesInConstructorNoAttributes : IAssertEquality
    {
        private readonly Guid Id;
        private readonly DateTime Date;
        private readonly string Text;
        private readonly int Number;
        private readonly Uri Uri;

        public PublicReadonlyFieldWithNamesInConstructorNoAttributes(Guid id, DateTime date, string text, int number, Uri uri)
        {
            this.Id = id;
            this.Date = date;
            this.Text = text;
            this.Number = number;
            this.Uri = uri;
        }

        public static PublicReadonlyFieldWithNamesInConstructorNoAttributes CreateInstance()
        {
            return new PublicReadonlyFieldWithNamesInConstructorNoAttributes(Guid.NewGuid(), DateTime.Now.Date.AddSeconds(543).ToLocalTime(), "test", 10, new Uri("http://www.google.com"));

        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PublicReadonlyFieldWithNamesInConstructorNoAttributes>();

            PublicReadonlyFieldWithNamesInConstructorNoAttributes target = other as PublicReadonlyFieldWithNamesInConstructorNoAttributes;

            target.Id.Should().Be(this.Id);
            target.Date.ToUniversalTime().Should().BeCloseTo(this.Date.ToUniversalTime());
            target.Text.Should().Be(this.Text);
            target.Number.Should().Be(this.Number);
            target.Uri.Should().Be(this.Uri);
        }

    }
}
