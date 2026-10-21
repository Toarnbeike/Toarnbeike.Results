namespace Toarnbeike.Results.Rules.UtcNow;

/// <summary>
/// Defines a provider for obtaining the current UTC date and time.
/// </summary>
internal interface IUctNowProvider
{
    /// <summary>
    /// Gets the current UTC date and time as a specified type.
    /// </summary>
    /// <typeparam name="TDate">The type of the date to return.</typeparam>
    /// <exception cref="NotSupportedException">Thrown when the specified type is not supported.</exception>
    /// <returns>The current UTC date and time as the specified type.</returns>
    public TDate GetUtcNow<TDate>() where TDate : IComparable<TDate>;
}
