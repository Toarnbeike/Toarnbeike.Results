namespace Toarnbeike.Results.Guards;

/// <summary>
/// The value on which rules are applied
/// </summary>
/// <typeparam name="T">The type of the guarded value.</typeparam>
public interface IGuardTarget<out T>
{
    /// <summary>
    /// Gets the guarded value.
    /// </summary>
    internal T Value { get; }

    /// <summary>
    /// Gets the captured caller argument expression for the guarded value.
    /// </summary>
    internal string Expression { get; }

    /// <summary>
    /// Access to the Tolerance provider, as provided by the GuardInitializer
    /// </summary>
    internal IGuardContext GuardContext { get; }
}