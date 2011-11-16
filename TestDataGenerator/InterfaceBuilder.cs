
namespace TestDataGenerator
{
	using System;
	using System.Linq;

	public class InterfaceBuilder : IBuildInstances
	{
		private Catalog catalog;

		public InterfaceBuilder(Catalog catalog)
		{
			this.catalog = catalog;
		}

		public bool CanCreate(Type type)
		{
			return type.IsInterface || type.IsAbstract;
		}

		public object CreateInstance(Type type, string name)
		{
			var candidates = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
				.Where(t => type.IsAssignableFrom(t))
				.Where(t => !t.IsInterface || t.IsAbstract);

			return catalog.CreateInstance(Rnd.RandomFromCollection(candidates));
		}
	}
}
