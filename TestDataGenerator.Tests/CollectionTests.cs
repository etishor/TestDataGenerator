using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace TestDataGenerator.Tests
{

    public class CollectionTests
    {
        [Fact]
        public void Catalog_Can_Create_Array()
        {
            Catalog catalog = new Catalog();
            Type array = typeof(string[]);

            object result = catalog.CreateInstance(array);

            Assert.IsType(array, result);
        }

        [Fact]
        public void Builder_Can_Create_Array()
        {
            Catalog catalog = new Catalog();
            CollectionsBuilder builder = new CollectionsBuilder(catalog);

            Type array = typeof(string[]);

            object result = builder.CreateInstance(array, null);

            Assert.IsType(array, result);
        }

        [Fact]
        public void Catalog_Can_Create_Non_Empty_Array()
        {
            Catalog catalog = new Catalog();

            List<string[]> resultedLists = new List<string[]>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty list 
            {
                object result = catalog.CreateInstance(typeof(string[]));
                Assert.IsType<string[]>(result);
                resultedLists.Add(result as string[]);
            }
            Assert.True(resultedLists.Any(l => l.Any()));
        }

        [Fact]
        public void Catalog_Can_Create_Generic_Enumerable()
        {
            new Catalog().CreateInstance(typeof(IEnumerable<string>))
                .Should().BeAssignableTo<IEnumerable<string>>();
        }

        [Fact]
        public void Builder_Can_Create_Generic_Enumerable()
        {
            Catalog catalog = new Catalog();
            CollectionsBuilder builder = new CollectionsBuilder(catalog);

            Type enumerable = typeof(IEnumerable<string>);

            object result = builder.CreateInstance(enumerable, null);

            result.Should().BeAssignableTo<IEnumerable<string>>();
        }

        [Fact]
        public void Catalog_Can_Create_List()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(List<string>));
            Assert.IsType<List<string>>(result);
        }

        [Fact]
        public void Catalog_Can_Create_Non_Empty_List()
        {
            Catalog catalog = new Catalog();

            List<List<string>> resultedLists = new List<List<string>>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty list 
            {
                object result = catalog.CreateInstance(typeof(List<string>));
                Assert.IsType<List<string>>(result);
                resultedLists.Add(result as List<string>);
            }
            Assert.True(resultedLists.Any(l => l.Any()));
        }

        [Fact]
        public void Catalog_Can_Create_IList()
        {
            new Catalog().CreateInstance(typeof(IList<string>))
                .Should().BeAssignableTo<IList<string>>();
        }

        [Fact]
        public void Catalog_Can_Create_ISet()
        {
            new Catalog().CreateInstance(typeof(ISet<string>))
                .Should().BeAssignableTo<ISet<string>>();
        }

        [Fact]
        public void Catalog_Can_Create_HashedSet()
        {
            new Catalog().CreateInstance(typeof(HashSet<string>))
                .Should().BeOfType<HashSet<string>>();
        }

        [Fact]
        public void Catalog_Can_Create_Non_Empty_HashedSet()
        {
            Catalog catalog = new Catalog();

            List<HashSet<string>> resultedLists = new List<HashSet<string>>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty list 
            {
                object result = catalog.CreateInstance(typeof(HashSet<string>));
                Assert.IsType<HashSet<string>>(result);
                resultedLists.Add(result as HashSet<string>);
            }
            Assert.True(resultedLists.Any(l => l.Any()));
        }


        [Fact]
        public void Catalog_Can_Create_Dictionary()
        {
            new Catalog().CreateInstance(typeof(Dictionary<int, string>))
                .Should().BeOfType<Dictionary<int, string>>();
        }

        [Fact]
        public void Catalog_Can_Create_Non_Empty_Dictionary()
        {
            Catalog catalog = new Catalog();

            List<Dictionary<int, string>> resultedLists = new List<Dictionary<int, string>>();
            for (int i = 0; i < 100; i++) // 100 times since rnd might generate an empty dictionary 
            {
                object result = catalog.CreateInstance(typeof(Dictionary<int, string>));
                Assert.IsType<Dictionary<int, string>>(result);
                resultedLists.Add(result as Dictionary<int, string>);
            }
            Assert.True(resultedLists.Any(l => l.Any()));
        }

        [Fact]
        public void Catalog_Can_Create_IDictionary()
        {
            new Catalog().CreateInstance(typeof(IDictionary<int, string>))
                .Should().BeAssignableTo<IDictionary<int, string>>();
        }

        class TestClass
        {
            public IDictionary<string, List<string>> Value { get; set; }
        }

        [Fact]
        public void Catalog_Can_Create_Object_With_Composed_Collection()
        {
            Catalog catalog = new Catalog();
            object result = catalog.CreateInstance(typeof(TestClass));

            result.Should().BeOfType<TestClass>();

            TestClass instance = result as TestClass;
            instance.Value.Should().NotBeNull();
        }
    }
}
