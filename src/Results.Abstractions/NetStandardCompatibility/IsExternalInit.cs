using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Allows records with init-only properties to be used in .NET Standard 2.0 by providing a definition for the <c>IsExternalInit</c> type that the C# compiler looks for when compiling records with init-only properties.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class IsExternalInit;
