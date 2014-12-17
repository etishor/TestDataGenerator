namespace TestDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Default instance builder, used for any Type not configured with other builder.
    /// </summary>
    public class TypeBuilder : IBuildInstances
    {
        private readonly Catalog catalog;
        private readonly Type type;
        private Func<object> instanceCreator;
        private List<Action<object>> postConfiguration = new List<Action<object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class.
        /// </summary>
        /// <param name="catalog">The global catalog used to build references.</param>
        /// <param name="type">The type this builder creates.</param>
        public TypeBuilder(Catalog catalog, Type type)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException("catalog");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            this.catalog = catalog;
            this.type = type;
        }

        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanCreate(Type type)
        {
            return this.type == type;
        }

        /// <summary>
        /// Creates an instance of the specified type. The <paramref name="name"/> is passed to data generators as a hint to
        /// generate a more meaningful value.
        /// </summary>
        /// <param name="type">The type of the instance to create.</param>
        /// <param name="name">The name, usually the property name.</param>
        /// <returns>
        /// Then newly created instance.
        /// </returns>
        public object CreateInstance(Type type, string name)
        {
            return this.CreateInstance(name);
        }


        public TypeBuilder WithConstructor(Func<object> constructor)
        {
            this.EnsureConstructorIsNotConfigured();
            this.instanceCreator = () =>
            {
                object instance = constructor();
                if (instance == null || !this.type.IsAssignableFrom(instance.GetType()))
                {
                    throw new InvalidOperationException("Invalid instance returned by calling constructor");
                }
                return instance;
            };
            return this;
        }


        public TypeBuilder WithConstructorWithLeastParameters()
        {
            this.EnsureConstructorIsNotConfigured();

            var constructor = this.type.GetConstructors().OrderBy(c => c.GetParameters().Length).FirstOrDefault();

            if (constructor == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find a constructor for type '{0}'", type.FullName));
            }

            this.instanceCreator = () => InvokeConstructor(constructor);

            return this;
        }

        public TypeBuilder WithConstructorWithMostParameters()
        {
            this.EnsureConstructorIsNotConfigured();

            var constructor = this.type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
            if (constructor == null)
            {
                throw new InvalidOperationException("Unable to find a constructor");
            }

            this.instanceCreator = () => InvokeConstructor(constructor);

            return this;
        }

        public TypeBuilder WithPostConstruction(Action<object> postConstruction)
        {
            this.postConfiguration.Add(postConstruction);
            return this;
        }

        protected object CreateInstance(string name)
        {
            if (this.instanceCreator == null)
            {
                this.WithConstructorWithLeastParameters();
            }

            if (this.instanceCreator == null)
            {
                throw new InvalidOperationException("You must configure the way the instance is constructed.");
            }

            var instance = instanceCreator();

            SetProperties(instance);

            this.postConfiguration.ForEach(a => a(instance));

            return instance;
        }

        private void EnsureConstructorIsNotConfigured()
        {
            if (this.instanceCreator != null)
            {
                throw new InvalidOperationException("Constructor must be specified just one time.");
            }
        }

        private void SetProperties(object instance)
        {

            var props = this.type.GetProperties().Where(p => p.CanWrite && p.GetIndexParameters().Length == 0);
            foreach (var prop in props)
            {
                object val = this.catalog.CreateInstance(prop.PropertyType, prop.Name);
                prop.SetValue(instance, val, null);
            }
        }

        private object InvokeConstructor(ConstructorInfo constructorInfo)
        {
            var args = constructorInfo.GetParameters().Select(p => this.catalog.CreateInstance(p.ParameterType, p.Name));
            return constructorInfo.Invoke(args.ToArray());
        }

    }
}
