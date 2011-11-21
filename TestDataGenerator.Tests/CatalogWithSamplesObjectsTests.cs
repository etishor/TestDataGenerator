using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json.Bson;

namespace TestDataGenerator.Tests
{	
	public class CatalogWithSamplesObjectsTests
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

		private static void RunTest(Type message)
		{
			Catalog catalog = new Catalog();
            IAssertEquality instance = (IAssertEquality)catalog.CreateInstance(message);
			Assert.IsInstanceOfType(message, instance);
                        
            ObjectDataTree messageTree = new ObjectDataTree(instance);

            using (MemoryStream ms = new MemoryStream())
            {
                Serialize(new IndisposableStream(ms), instance);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                object result = Deserialize(new IndisposableStream(ms), message);

                ObjectDataTree resultTree = new ObjectDataTree(result);
                Assert.AreEqual(messageTree.StringValue(), resultTree.StringValue());
                
                instance.AssertEquality(result);
            }

		}

		public static IEnumerable<Type> GetMessages()
		{
			return typeof(IAssertEquality).Assembly.GetTypes()
				.Where(t => !t.IsInterface)
				.Where(t => typeof(IAssertEquality).IsAssignableFrom(t));
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
	}
}
