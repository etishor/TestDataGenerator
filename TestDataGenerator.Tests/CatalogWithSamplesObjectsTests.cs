using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;

namespace TestDataGenerator.Tests
{	
	public class CatalogWithSamplesObjectsTests
	{
		[StaticTestFactory]
		public static IEnumerable<Test> CreateTests()
		{
			foreach (Type message in GetMessages())
			{
				TestCase test = new TestCase(message.Name, () => RunTest(message));
				yield return test;
			}
		}

		private static void RunTest(Type message)
		{
			Catalog catalog = new Catalog();
			object instance = catalog.CreateInstance(message);
			Assert.IsInstanceOfType(message, instance);
		}

		public static IEnumerable<Type> GetMessages()
		{
			return typeof(IAssertEquality).Assembly.GetTypes()
				.Where(t => !t.IsInterface)
				.Where(t => typeof(IAssertEquality).IsAssignableFrom(t));
		}
	}
}
