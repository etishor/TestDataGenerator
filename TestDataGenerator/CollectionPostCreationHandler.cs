// -----------------------------------------------------------------------
// <copyright file="CollectionPostCreationHandler.cs" company="Recognos Romania">
// {RecognosCopyrightTextPlaceholder}
// </copyright>
// -----------------------------------------------------------------------

namespace TestDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Fills instances that have collection like behavior with elements. 
    /// </summary>
    public class CollectionPostCreationHandler : IPostCreationHandler
    {
        private const int CollectionLimit = 10;
        private const int MinCollectionLimit = 1;

        private readonly Catalog catalog;

        public CollectionPostCreationHandler(Catalog catalog)
        {
            this.catalog = catalog;
        }

        public void ApplyPostCreationRule(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Type instanceType = instance.GetType();

            if (!instanceType.IsGenericType)
            {
                return;
            }

            Type genericType = instanceType.GetGenericTypeDefinition();

            if (genericType.GetInterface(typeof(ICollection<>).Name) == null)
            {
                return;
            }

            var count = instanceType.GetProperty("Count");
            int countValue = (int)count.GetValue(instance, null);

            // the collection has already been filled by the builder
            if (countValue > 0)
            {
                return;
            }

            Type[] elements = instanceType.GetGenericArguments();

            // TODO: handle custom dictionaries
            if (elements.Count() > 1)
            {
                return;
            }

            Type elementType = elements.Single();

            var add = instanceType.GetMethod("Add");

            for (int i = 0; i < Rnd.Integer(MinCollectionLimit, CollectionLimit); i++)
            {
                add.Invoke(instance, new object[] { this.catalog.CreateInstance(elementType) });
            }
        }
    }
}
