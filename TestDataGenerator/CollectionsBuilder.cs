using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDataGenerator
{
    public class CollectionsBuilder : IBuildInstances
    {
        private readonly Catalog catalog;

        public CollectionsBuilder(Catalog catalog)
        {
            this.catalog = catalog;
        }

        public bool CanCreate(Type type)
        {
            if (type.IsArray)
            {
                return true;
            }

            if (!type.IsGenericType)
            {
                return false;
            }

            Type generic = type.GetGenericTypeDefinition();

            return generic == typeof(IEnumerable<>) ||
                generic == typeof(ICollection<>) ||
                generic == typeof(IList<>) ||
                generic == typeof(ISet<>) ||
                generic == typeof(IDictionary<,>);
        }

        public object CreateInstance(Type type, string name)
        {
            if (type.IsArray)
            {
                return CreateArray(type);
            }

            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                if (genericType == typeof(IEnumerable<>))
                {
                    Type argument = type.GetGenericArguments().Single();
                    return CreateArrayFromElement(argument);
                }

                if (genericType == typeof(ICollection<>) || genericType == typeof(IList<>))
                {
                    Type argument = type.GetGenericArguments().Single();
                    return CreateListFromElement(argument);
                }

                if (genericType == typeof(ISet<>))
                {
                    Type argument = type.GetGenericArguments().Single();
                    return CreateSetFromElement(argument);
                }

                if (genericType == typeof(IDictionary<,>))
                {
                    Type[] arguments = type.GetGenericArguments();
                    return CreateDictionaryFromElements(arguments[0], arguments[1]);
                }
            }

            throw new InvalidOperationException("Unsupported Collection. You need to specify a custom builder.");
        }

        private object CreateListFromElement(Type element)
        {
            Type list = typeof(List<>).MakeGenericType(element);
            object instance = this.catalog.CreateInstance(list);

            var add = list.GetMethod("Add");

            for (int i = 0; i < Rnd.Integer(10); i++)
            {
                add.Invoke(instance, new object[] { this.catalog.CreateInstance(element) });
            }
            return instance;
        }

        private object CreateSetFromElement(Type element)
        {
            Type set = typeof(HashSet<>).MakeGenericType(element);
            object instance = this.catalog.CreateInstance(set);

            var add = set.GetMethod("Add");

            for (int i = 0; i < Rnd.Integer(10); i++)
            {
                add.Invoke(instance, new object[] { this.catalog.CreateInstance(element, i.ToString()) });
            }
            return instance;
        }

        private object CreateDictionaryFromElements(Type keyType, Type valueType)
        {
            Type dictionary = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            object instance = this.catalog.CreateInstance(dictionary);

            var add = dictionary.GetMethod("Add");

            for (int i = 0; i < Rnd.Integer(10); i++)
            {
                add.Invoke(instance, new object[] 
                { 
                    this.catalog.CreateInstance(keyType, i.ToString()), 
                    this.catalog.CreateInstance(valueType,i.ToString())
                });
            }

            return instance;
        }

        private object CreateArrayFromElement(Type element)
        {
            return CreateArray(element.MakeArrayType());
        }

        /// <summary>
        /// Creates the an array of type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the array to create.</param>
        /// <returns>The new array.</returns>
        private object CreateArray(Type type)
        {
            if (!type.IsArray)
            {
                throw new InvalidOperationException("Type must be an array");
            }

            Type element = type.GetElementType();
            int len = Rnd.Integer(10);

            // arrays only have one constructor
            var array = type.GetConstructors().Single().Invoke(new object[] { len });

            var setter = type.GetMethod("SetValue", new Type[] { typeof(object), typeof(int) });

            for (int i = 0; i < len; i++)
            {
                setter.Invoke(array, new object[] { this.catalog.CreateInstance(element), i });
            }
            return array;
        }
    }
}
