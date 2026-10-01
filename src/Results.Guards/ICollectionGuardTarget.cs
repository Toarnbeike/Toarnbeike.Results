namespace Toarnbeike.Results.Guards;

/// <summary>
/// The collection of values on which rules are applied
/// </summary>
/// <typeparam name="TElement">The type of the guarded element.</typeparam>
public interface ICollectionGuardTarget<TElement>
{
    /// <summary>
    /// Gets the number of elements in the collection.
    /// </summary>
    internal IList<TElement> Values { get; }

    /// <summary>
    /// Gets the captured caller argument expression for the guarded value.
    /// </summary>
    internal string Expression { get; }

    /// <summary>
    /// Access to the Tolerance provider, as provided by the GuardInitializer
    /// </summary>
    internal IGuardContext GuardContext { get; }
}