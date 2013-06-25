using System;
using System.Runtime.Serialization;
using Xunit;
using FluentAssertions;


namespace TestDataGenerator.Tests.Samples
{



    public class BaseObject
    {
        public BaseObject(string field, int property)
        {
            this.baseField = field;
            this.BaseProperty = property;
        }



        private string baseField;



        public int BaseProperty { get; protected set; }

        public void AssertBaseEquality(BaseObject target)
        {
            target.baseField.Should().Be(this.baseField);
            target.BaseProperty.Should().Be(this.BaseProperty);
        }
    }




    public class InheritedObject : BaseObject, IAssertEquality
    {


        private Guid field;

        public InheritedObject(Guid field, string baseField, int baseProperty)
            : base(baseField, baseProperty)
        {
            this.field = field;
        }



        public Uri Uri { get; set; }


        public static InheritedObject CreateInstance()
        {
            return new InheritedObject(Guid.NewGuid(), "base", 10) { Uri = new Uri("http://www.google.com") };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<InheritedObject>();

            InheritedObject target = other as InheritedObject;

            target.AssertBaseEquality(other as BaseObject);

            target.field.Should().Be(this.field);
            target.Uri.Should().Be(this.Uri);
        }
    }
}
