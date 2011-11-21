﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MbUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;
using TestDataGenerator.Tests.Samples;

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
            return GetMessages().Select(m => new TestCase(m.Name, () => RunTest(m)));
        }

        private static void RunTest(Type message)
        {
            Catalog catalog = new Catalog();
            catalog.RegisterBuilder<ObjectPropertyWithCustomValue>().WithPostConstruction(o => ((ObjectPropertyWithCustomValue)o).Value = catalog.CreateInstance<CustomObject>());


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
