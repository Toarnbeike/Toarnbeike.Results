using System.Globalization;
using System.Numerics;
using Toarnbeike.Results.Rules.Messages;
using Toarnbeike.Results.Rules.Tolerance;
using Toarnbeike.Results.Rules.UtcNow;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Results.Rules.Tests")]

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Provides a static context for rule evaluations.
/// Contains settings regarding the UTC time provider, floating point tolerances and message formatting.
/// </summary>
public static class RuleContext
{
    /// <summary>
    /// Use the system's current UTC time as context for rule evaluations.
    /// </summary>
    public static void UseSystemUtcNow() => 
        UtcNowProvider = new SystemUtcNowProvider();

    /// <summary>
    /// Use a fixed UTC time as context for rule evaluations.
    /// Useful for testing scenarios where a consistent time reference is needed.
    /// </summary>
    /// <param name="value">The fixed UTC time to use.</param>
    public static void UseFixedUtcNow(DateTimeOffset value) => 
        UtcNowProvider = new StaticUtcNowProvider(value);

    /// <summary>
    /// Use the default tolerances for floating point comparisons as context for rule evaluations.
    /// </summary>
    public static void UseDefaultTolerance() =>
        ToleranceProvider = new DefaultToleranceProvider();

    /// <summary>
    /// Use zero tolerance for floating point comparisons as context for rule evaluations.
    /// </summary>
    public static void UseZeroTolerance() =>
        ToleranceProvider = new ZeroToleranceProvider();

    /// <summary>
    /// Use a custom tolerance provider for floating point comparisons as context for rule evaluations.
    /// </summary>
    /// <param name="provider">The custom tolerance provider to use.</param>
    public static void UseCustomToleranceProvider(IToleranceProvider provider) =>
        ToleranceProvider = provider;

    /// <summary>
    /// Use the default message provider for rule evaluations, which provides standard messages in English for rule violations.
    /// </summary>
    public static void UseDefaultMessageProvider() =>
        MessageProvider = new DefaultRuleMessageProvider();

    /// <summary>
    /// Use a custom message provider for rule evaluations, allowing for customized (localized) messages for rule violations.
    /// </summary>
    /// <param name="provider">The custom message provider to use.</param>
    public static void UseCustomMessageProvider(IRuleMessageProvider provider) =>
        MessageProvider = provider;

    /// <summary>
    /// Use the invariant culture for formatting messages and values in rule evaluations, ensuring consistent formatting regardless of the system's locale settings.
    /// </summary>
    public static void UseInvariantCulture() =>
        CurrentCulture = CultureInfo.InvariantCulture;

    /// <summary>
    /// Use a specific culture for formatting messages and values in rule evaluations.
    /// </summary>
    /// <param name="culture">The culture to use.</param>
    public static void UseCulture(CultureInfo culture) =>
        CurrentCulture = culture;

    /// <summary>
    /// Use the system's current culture for formatting messages and values in rule evaluations, allowing for localized formatting based on the system's locale settings.
    /// </summary>
    public static void UseSystemCulture() =>
        CurrentCulture = CultureInfo.CurrentCulture;

    /// <summary>
    /// Gets the message provider for rule evaluations, as configured by the context.
    /// </summary>
    internal static IRuleMessageProvider MessageProvider { get; private set; } = new DefaultRuleMessageProvider();

    /// <summary>
    /// Gets the current culture for formatting messages and values in rule evaluations, as configured by the context.
    /// </summary>
    internal static CultureInfo CurrentCulture { get; private set; } = CultureInfo.InvariantCulture;

    private static IUctNowProvider UtcNowProvider { get; set; } = new SystemUtcNowProvider();

    /// <summary>
    /// Gets the current UTC time as context for rule evaluations, as configured by the context.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown when the <typeparamref name="TDate"/> type is not a supported Date/Time object.</exception>
    internal static TDate UtcNow<TDate>() where TDate : IComparable<TDate> =>
        UtcNowProvider.GetUtcNow<TDate>();

    private static IToleranceProvider ToleranceProvider { get; set; } = new DefaultToleranceProvider();

    /// <summary>
    /// Gets the tolerance for a given numeric type, as configured by the context.
    /// </summary>
    internal static TNumber Tolerance<TNumber>() where TNumber : INumber<TNumber> =>
        ToleranceProvider.GetTolerance<TNumber>();
}
