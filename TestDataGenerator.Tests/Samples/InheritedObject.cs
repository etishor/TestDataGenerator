using System;
using System.Runtime.Serialization;
using MbUnit.Framework;


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
			Assert.AreEqual(this.baseField, target.baseField);
			Assert.AreEqual(this.BaseProperty, target.BaseProperty);
		}
	}

	
	
	
	public class InheritedObject  : BaseObject, IAssertEquality
	{
		
		
		private Guid field;
		
		public InheritedObject(Guid field, string baseField, int baseProperty)
			:base(baseField,baseProperty)
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
			Assert.IsNotNull(other);
			Assert.IsInstanceOfType<InheritedObject>(other);

			InheritedObject target = other as InheritedObject;

			target.AssertBaseEquality(other as BaseObject);
			
			Assert.AreEqual(this.field, target.field);
			Assert.AreEqual(this.Uri, target.Uri);
		}
	}
}
