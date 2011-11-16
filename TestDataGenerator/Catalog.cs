
namespace TestDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Catalog
    {
        private readonly IList<IBuildInstances> builders = new List<IBuildInstances>();

		private int recursionLevel = 0;

        public Catalog()
        {
			this.MaxRecursionDepth = 10;

			// from generic to specific - builders are called in reverse order until one can create the type
			this.RegisterCustomBuilder(new InterfaceBuilder(this));

            this.RegisterCustomBuilder(new CollectionsBuilder(this));

			this.RegisterCustomBuilder(new PrimitieveBuilder());
        }

		public int MaxRecursionDepth { get; set; }

        public GenericTypeBuilder<T> RegisterBuilder<T>()
        {
            return this.RegisterCustomBuilder(new GenericTypeBuilder<T>(this)) as GenericTypeBuilder<T>;
        }

        public TypeBuilder RegisterBuilder(Type type)
        {
            return this.RegisterCustomBuilder(new TypeBuilder(this, type)) as TypeBuilder;
        }

        public IBuildInstances RegisterCustomBuilder(IBuildInstances builder)
        {
            if (builders == null)
            {
                throw new ArgumentNullException("builder");
            }

            this.builders.Add(builder);
            return builder;
        }

        public object CreateInstance(Type type, string name = "")
        {
			if (this.recursionLevel > this.MaxRecursionDepth)
			{
				return null;
			}

			this.recursionLevel++;
			try
			{
				IBuildInstances builder = this.builders.Reverse().Where(b => b.CanCreate(type)).FirstOrDefault();

				if (builder == null)
				{
					builder = RegisterBuilder(type);
				}

				if (builder == null)
				{
					throw new InvalidOperationException(string.Format("Unable to find a suitable builder for type {0}", type));
				}

				return builder.CreateInstance(type, name);
			}
			finally
			{
				this.recursionLevel--;
			}
        }

        public T CreateInstance<T>(string name = "")
        {
            return (T)CreateInstance(typeof(T), name);
        }
    }
}
