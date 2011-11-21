
namespace TestDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Main class of the generator.
    /// This class has a list of instance builders and can use them to populate an instance of an object. 
    /// </summary>
    /// <remarks>
    /// Builders are tried in reverse order of the registration.
    /// </remarks>
    public class Catalog
    {
        private readonly IList<IBuildInstances> builders = new List<IBuildInstances>();
        private readonly List<IPostCreationHandler> postCreationHandlers = new List<IPostCreationHandler>();

        private int recursionLevel = 0;

        public Catalog()
        {
            this.MaxRecursionDepth = 10;

            // from generic to specific - builders are called in reverse order until one can create the type
            this.RegisterCustomBuilder(new InterfaceBuilder(this));

            this.RegisterCustomBuilder(new CollectionsBuilder(this));

            this.RegisterCustomBuilder(new PrimitieveBuilder());

            this.RegisterPostCreationHandler(new CollectionPostCreationHandler(this));
        }

        /// <summary>
        /// Gets or sets the max recursion depth for creating an object graph.
        /// </summary>
        /// <value>
        /// The max recursion depth.
        /// </value>
        public int MaxRecursionDepth { get; set; }

        /// <summary>
        /// Registers the builder for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type for which the builder is registered.</typeparam>
        /// <returns>The registered builder for fluent chaining of the configuration.</returns>
        public GenericTypeBuilder<T> RegisterBuilder<T>()
        {
            return this.RegisterCustomBuilder(new GenericTypeBuilder<T>(this)) as GenericTypeBuilder<T>;
        }

        /// <summary>
        /// Registers the builder for type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">Type for which the builder is registered.</param>
        /// <returns>The registered builder for fluent chaining of the configuration.</returns>
        public TypeBuilder RegisterBuilder(Type type)
        {
            return this.RegisterCustomBuilder(new TypeBuilder(this, type)) as TypeBuilder;
        }

        /// <summary>
        /// Registers the custom builder.
        /// </summary>
        /// <typeparam name="T">Type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The registered builder for fluent chaining of the configuration.</returns>
        public T RegisterCustomBuilder<T>(T builder)
            where T : IBuildInstances
        {
            if (builders == null)
            {
                throw new ArgumentNullException("builder");
            }

            this.builders.Add(builder);
            return builder;
        }

        public T RegisterPostCreationHandler<T>(T handler)
            where T : IPostCreationHandler
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.postCreationHandlers.Add(handler);
            return handler;
        }

        /// <summary>
        /// Creates an instance of <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of instance to create.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>The newly created instance.</returns>
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

                object instance = builder.CreateInstance(type, name);

                postCreationHandlers.ForEach(h => h.ApplyPostCreationRule(instance));

                return instance;
            }
            finally
            {
                this.recursionLevel--;
            }
        }

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the instance to create.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The newly created instance.</returns>
        public T CreateInstance<T>(string name = "")
        {
            return (T)CreateInstance(typeof(T), name);
        }
    }
}
