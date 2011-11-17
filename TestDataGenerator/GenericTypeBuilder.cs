using System;
namespace TestDataGenerator
{

    /// <summary>
    /// Wrapper around TypeBuilder which uses generics.
    /// </summary>
    public class GenericTypeBuilder<T> : TypeBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTypeBuilder&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="catalog">The global catalog used for references.</param>
        public GenericTypeBuilder(Catalog catalog)
            : base(catalog, typeof(T))
        {
        }

        public new T CreateInstance(string name)
        {
            return (T)base.CreateInstance(name);
        }

        public GenericTypeBuilder<T> WithConstructor(Func<T> constructor)
        {
            return base.WithConstructor(() => constructor()) as GenericTypeBuilder<T>;
        }

        public GenericTypeBuilder<T> WithPostConstruction(Action<T> postConstruction)
        {
            return (GenericTypeBuilder<T>)base.WithPostConstruction(o => postConstruction((T)o));
        }
    }
}
