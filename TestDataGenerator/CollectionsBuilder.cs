﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDataGenerator
{
    public class CollectionsBuilder : IBuildInstances
    {
        private const int CollectionLimit = 10;
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
                generic == typeof(IList<>) || generic == typeof(List<>) ||
                generic == typeof(ISet<>) || generic == typeof(HashSet<>) ||
                generic == typeof(IDictionary<,>) || generic == typeof(Dictionary<,>);
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

                if (genericType == typeof(IEnumerable<>) || genericType == typeof(ICollection<>) || genericType == typeof(IList<>) || genericType == typeof(List<>))
                {
                    Type argument = type.GetGenericArguments().Single();
                    return CreateListFromElement(argument);
                }

                if (genericType == typeof(ISet<>) || genericType == typeof(HashSet<>))
                {
                    Type argument = type.GetGenericArguments().Single();
                    return CreateSetFromElement(argument);
                }

                if (genericType == typeof(IDictionary<,>) || genericType == typeof(Dictionary<,>))
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
            return Activator.CreateInstance(list);
        }

        private object CreateSetFromElement(Type element)
        {
            Type set = typeof(HashSet<>).MakeGenericType(element);
            object instance = Activator.CreateInstance(set);

            var add = set.GetMethod("Add");
            var container = set.GetMethod("Contains");

            for (int i = 0; i < Rnd.Integer(CollectionLimit); i++)
            {
                object el = this.catalog.CreateInstance(element, i.ToString());
                bool added = (bool)container.Invoke(instance, new object[] { el });
                if (!added)
                {
                    add.Invoke(instance, new object[] { el });
                }
            }
            return instance;
        }

        private object CreateDictionaryFromElements(Type keyType, Type valueType)
        {
            Type dictionary = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            object instance = Activator.CreateInstance(dictionary);

            var add = dictionary.GetMethod("Add");
            var contains = dictionary.GetMethod("ContainsKey");

            for (int i = 0; i < Rnd.Integer(CollectionLimit); i++)
            {
                object key = this.catalog.CreateInstance(keyType, i.ToString());
                object val = this.catalog.CreateInstance(valueType, i.ToString());

                bool added = (bool)contains.Invoke(instance, new object[] { key });
                if (!added)
                {
                    add.Invoke(instance, new object[] { key, val });
                }
            }

            return instance;
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
            int len = Rnd.Integer(CollectionLimit);

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
