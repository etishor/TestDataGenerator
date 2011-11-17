namespace TestDataGenerator
{
    using System;

    /// <summary>
    /// Indicates the ability to create instances for a set of types
    /// </summary>
    public interface IBuildInstances
    {
        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.
        /// </returns>
        bool CanCreate(Type type);

        /// <summary>
        /// Creates an instance of the specified type. The <paramref name="name"/> is passed to data generators as a hint to 
        /// generate a more meaningful value.
        /// </summary>
        /// <param name="type">The type of the instance to create.</param>
        /// <param name="name">The name, usually the property name.</param>
        /// <returns>Then newly created instance.</returns>
        object CreateInstance(Type type, string name);
    }
}
