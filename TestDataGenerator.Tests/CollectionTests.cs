using System;
using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;

namespace TestDataGenerator.Tests
{
    [TestFixture]
    public class CollectionTests
    {
        [Test]
        public void Catalog_Can_Create_Array()
        {
            Catalog catalog = new Catalog();
            Type array = typeof(string[]);

            object result = catalog.CreateInstance(array);

            Assert.IsInstanceOfType(array, result);
        }

        [Test]
        public void Builder_Can_Create_Array()
        {
            Catalog catalog = new Catalog();
            CollectionsBuilder builder = new CollectionsBuilder(catalog);

            Type array = typeof(string[]);

            object result = builder.CreateInstance(array, null);

            Assert.IsInstanceOfType(array, result);
        }

        [Test]
        public void Catalog_Can_Create_Non_Empty_Array()
        {
            Catalog catalog = new Catalog();

            List<string[]> resultedLists = new List<string[]>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty list 
            {
                object result = catalog.CreateInstance(typeof(string[]));
                Assert.IsInstanceOfType<string[]>(result);
                resultedLists.Add(result as string[]);
            }
            Assert.IsTrue(resultedLists.Any(l => l.Any()));
        }

        [Test]
        public void Catalog_Can_Create_Generic_Enumerable()
        {
            Catalog catalog = new Catalog();
            Type enumerable = typeof(IEnumerable<string>);

            object result = catalog.CreateInstance(enumerable);

            Assert.IsInstanceOfType(enumerable, result);
        }

        [Test]
        public void Builder_Can_Create_Generic_Enumerable()
        {
            Catalog catalog = new Catalog();
            CollectionsBuilder builder = new CollectionsBuilder(catalog);

            Type enumerable = typeof(IEnumerable<string>);

            object result = builder.CreateInstance(enumerable, null);

            Assert.IsInstanceOfType(enumerable, result);
        }

        [Test]
        public void Catalog_Can_Create_List()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(List<string>));
            Assert.IsInstanceOfType<List<string>>(result);
        }

        [Test]
        public void Catalog_Can_Create_Non_Empty_List()
        {
            Catalog catalog = new Catalog();

            List<List<string>> resultedLists = new List<List<string>>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty list 
            {
                object result = catalog.CreateInstance(typeof(List<string>));
                Assert.IsInstanceOfType<List<string>>(result);
                resultedLists.Add(result as List<string>);
            }
            Assert.IsTrue(resultedLists.Any(l => l.Any()));
        }

        [Test]
        public void Catalog_Can_Create_IList()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(IList<string>));
            Assert.IsInstanceOfType<IList<string>>(result);
        }

        [Test]
        public void Catalog_Can_Create_ISet()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(ISet<string>));
            Assert.IsInstanceOfType<ISet<string>>(result);
        }

        [Test]
        public void Catalog_Can_Create_HashedSet()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(HashSet<string>));
            Assert.IsInstanceOfType<HashSet<string>>(result);
        }

        [Test]
        public void Catalog_Can_Create_Non_Empty_HashedSet()
        {
            Catalog catalog = new Catalog();

            List<HashSet<string>> resultedLists = new List<HashSet<string>>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty list 
            {
                object result = catalog.CreateInstance(typeof(HashSet<string>));
                Assert.IsInstanceOfType<HashSet<string>>(result);
                resultedLists.Add(result as HashSet<string>);
            }
            Assert.IsTrue(resultedLists.Any(l => l.Any()));
        }


        [Test]
        public void Catalog_Can_Create_Dictionary()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(Dictionary<int, string>));
            Assert.IsInstanceOfType<Dictionary<int, string>>(result);
        }

        [Test]
        public void Catalog_Can_Create_Non_Empty_Dictionary()
        {
            Catalog catalog = new Catalog();

            List<Dictionary<int, string>> resultedLists = new List<Dictionary<int, string>>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty dictionary 
            {
                object result = catalog.CreateInstance(typeof(Dictionary<int, string>));
                Assert.IsInstanceOfType<Dictionary<int, string>>(result);
                resultedLists.Add(result as Dictionary<int, string>);
            }
            Assert.IsTrue(resultedLists.Any(l => l.Any()));
        }

        [Test]
        public void Catalog_Can_Create_IDictionary()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(IDictionary<int, string>));
            Assert.IsInstanceOfType<IDictionary<int, string>>(result);
        }
    }
}
