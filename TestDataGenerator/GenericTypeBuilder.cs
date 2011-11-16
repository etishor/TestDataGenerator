// -----------------------------------------------------------------------
// <copyright file="GenericTypeBuilder.cs" company="Recognos Romania">
// {RecognosCopyrightTextPlaceholder}
// </copyright>
// -----------------------------------------------------------------------

using System;
namespace TestDataGenerator
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GenericTypeBuilder<T> : TypeBuilder
    {
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
