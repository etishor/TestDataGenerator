namespace TestDataGenerator
{
    using System;

    /// <summary>
    /// Indicates the ability to create instances for a set of types
    /// </summary>
    public interface IBuildInstances
    {
        bool CanCreate(Type type);
        object CreateInstance(Type type, string name);
    }
}
