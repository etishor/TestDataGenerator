// -----------------------------------------------------------------------
// <copyright file="IPostCreationHandler.cs" company="Recognos Romania">
// {RecognosCopyrightTextPlaceholder}
// </copyright>
// -----------------------------------------------------------------------

namespace TestDataGenerator
{
    /// <summary>
    /// Indicates the ability to apply some rule after the instance has been created.
    /// </summary>
    public interface IPostCreationHandler
    {
        void ApplyPostCreationRule(object instance);
    }
}
