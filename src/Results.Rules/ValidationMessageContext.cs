using System.Globalization;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Context used for generating validation messages, including the culture for localization.
/// </summary>
/// <param name="PropertyName">The name of the property that was validated.</param>
/// <param name="Culture">The culture for localization.</param>
public sealed record ValidationMessageContext(string PropertyName, CultureInfo Culture)
{
    /// <summary>
    /// The index of a collection item, if applicable. Null if not part of a collection.
    /// </summary>
    public int? Index { get; private init; }

    /// <summary>
    /// Sets the index of a collection item for the validation message context.
    /// </summary>
    public ValidationMessageContext WithIndex(int index)
    {
        return this with { Index = index };
    }
};