namespace TestDataGenerator
{
	using System;
	using System.Collections.Generic;

	public class PrimitieveBuilder : IBuildInstances
	{
		private readonly IDictionary<Type, Func<string, object>> builders = new Dictionary<Type, Func<string, object>>();

		public PrimitieveBuilder()
		{
			this.builders.Add(typeof(bool), (s) => Rnd.Bool());

			this.builders.Add(typeof(int), (s) => Rnd.Integer());
			this.builders.Add(typeof(uint), (s) => Rnd.UInt());
			this.builders.Add(typeof(long), (s) => Rnd.Long());
			this.builders.Add(typeof(ulong), (s) => Rnd.ULong());
			this.builders.Add(typeof(byte), (s) => Rnd.Byte());
			this.builders.Add(typeof(short), (s) => Rnd.Short());
			

			this.builders.Add(typeof(char), (s) => Rnd.Char());

			this.builders.Add(typeof(DateTime), (s) => Rnd.Date());
			this.builders.Add(typeof(string), (s) => Rnd.String(s));
			this.builders.Add(typeof(Uri), (s) => Rnd.Uri(s));
		}

		public bool CanCreate(Type type)
		{
			return this.builders.ContainsKey(type) || type.IsEnum;			 
		}

		public object CreateInstance(Type type, string name)
		{
			if (!this.CanCreate(type))
			{
				throw new InvalidOperationException("Unable to create instance of type");
			}

			if (type.IsEnum)
			{
				return Rnd.RandomEnumValue(type);
			}

			return this.builders[type](name);
		}

	}
}
