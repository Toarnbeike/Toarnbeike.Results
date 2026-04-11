#if NETSTANDARD2_0
// ReSharper disable once CheckNamespace
namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Polyfill attribute to indicate that an output parameter is not null when the method returns a specified boolean value.
    /// Does nothing under .NET Standard 2.0, but allows the use of nullable reference types annotations in the public API of this library.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class NotNullWhenAttribute : Attribute
    {
        public NotNullWhenAttribute(bool returnValue) { }
    }
}
#endif