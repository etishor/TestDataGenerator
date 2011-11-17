using System;
using System.Collections.Generic;
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
        public void Catalog_Can_Create_Dictionary()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(Dictionary<int,string>));
            Assert.IsInstanceOfType<Dictionary<int, string>>(result);
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
