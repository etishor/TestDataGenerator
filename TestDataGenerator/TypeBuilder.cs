namespace TestDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TypeBuilder : IBuildInstances
    {
        private readonly Catalog catalog;
        private readonly Type type;
        private Func<object> instanceCreator;
        private List<Action<object>> postConfiguration = new List<Action<object>>();

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

        public bool CanCreate(Type type)
        {
            return this.type == type;
        }

        public object CreateInstance(Type type, string name)
        {
            return this.CreateInstance(name);
        }

        public object CreateInstance(string name)
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
                throw new InvalidOperationException("Unable to find a constructor");
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

        private void EnsureConstructorIsNotConfigured()
        {
            if (this.instanceCreator != null)
            {
                throw new InvalidOperationException("Constructor must be specified just one time.");
            }
        }

        private void SetProperties(object instance)
        {
            
            var props = this.type.GetProperties().Where(p => p.CanWrite && p.GetIndexParameters().Length ==0 );
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
