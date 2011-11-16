using System;
using System.Collections;
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
			return type.IsArray || typeof(IEnumerable).IsAssignableFrom(type);
		}

		public object CreateInstance(Type type, string name)
		{
			if (type.IsArray)
			{
				return CreateArray(type);
			}

			if(type.IsGenericType)
			{
				Type genericType = type.GetGenericTypeDefinition();
				if(genericType == typeof(IEnumerable<>))
				{
					Type argument = type.GetGenericArguments().Single();
					return CreateArray(argument.MakeArrayType());
				}
			}

			throw new InvalidOperationException("Unsupported Collection. You need to specify a custom builder.");
		}

		public object CreateArray(Type type)
		{
			if (!type.IsArray)
			{
				throw new InvalidOperationException("Type must be an array");
			}

			Type element = type.GetElementType();
			int len = Rnd.Integer(10);
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
