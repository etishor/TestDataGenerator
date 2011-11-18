using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MbUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;

namespace TestDataGenerator.Tests
{
	public class ObjectDataTreeWithSampleObjectsTests
	{
		private static JsonSerializer serializer = new JsonSerializer()
		{
			MissingMemberHandling = MissingMemberHandling.Error,
			TypeNameHandling = TypeNameHandling.All,
			ContractResolver = new DefaultContractResolver
			{
				DefaultMembersSearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
			}
		};

		[StaticTestFactory]
		public static IEnumerable<Test> CreateTests()
		{
			foreach (Type message in GetMessages())
			{
				TestCase test = new TestCase(message.Name, () => RunTest(message));
				yield return test;
			}
		}

		private static void RunTest(Type messageType)
		{
			IAssertEquality message = (IAssertEquality)messageType.GetMethod("CreateInstance",
				BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).Invoke(null, null);
			ObjectDataTree messageTree = new ObjectDataTree(message);

			using (MemoryStream ms = new MemoryStream())
			{
				Serialize(new IndisposableStream(ms), message);
				ms.Flush();
				ms.Seek(0, SeekOrigin.Begin);
				object result = Deserialize(new IndisposableStream(ms), messageType);
				ObjectDataTree resultTree = new ObjectDataTree(result);

				Assert.AreEqual(messageTree.StringValue(), resultTree.StringValue());
				message.AssertEquality(result);
			}
		}

		public static void Serialize(Stream stream, object instance)
		{
			using (var writer = new BsonWriter(stream))
			{
				serializer.Serialize(writer, instance);
			}
		}

		public static object Deserialize(Stream stream, System.Type type)
		{
			using (var reader = new BsonReader(stream))
			{
				return serializer.Deserialize(reader, type);
			}
		}

		public static IEnumerable<Type> GetMessages()
		{
			return typeof(IAssertEquality).Assembly.GetTypes()
				.Where(t => !t.IsInterface)
				.Where(t => typeof(IAssertEquality).IsAssignableFrom(t));
		}
	}
}
