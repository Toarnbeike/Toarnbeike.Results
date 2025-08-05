namespace Toarnbeike.Results.Failures;

/// <summary>
/// Represents a validation failure that combines multiple <see cref="ValidationFailure"/> instances
/// into a single <see cref="Failure"/>.
/// </summary>
/// <remarks>
/// Stores validation messages grouped by property name. 
/// Use <see cref="Failures"/> to retrieve all grouped failures, or <see cref="GetFailuresFor(string)"/> to retrieve messages for a specific property.
/// </remarks>
public sealed record ValidationFailures : Failure
{
    /// <summary>
    /// Gets all validation failures grouped by property name.
    /// </summary>
    public IReadOnlyDictionary<string, IReadOnlyList<string>> Failures { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFailures"/> class with a collection of failures.
    /// </summary>
    /// <param name="failures">The collection of validation failures to include.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="failures"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the collection is empty or contains null entries.</exception>
    public ValidationFailures(IEnumerable<ValidationFailure> failures)
        : base("validation_failures", "One or more validations failed:")
    {
        ArgumentNullException.ThrowIfNull(failures);

        var list = failures.ToList();
        if (list.Count == 0)
        {
            throw new ArgumentException("At least one validation failure must be provided.", nameof(failures));
        }

        if (list.Any(f => f is null))
        {
            throw new ArgumentException("Failures collection may not contain null elements.", nameof(failures));
        }

        Failures = list
            .GroupBy(f => f.Property)
            .ToDictionary(
                g => g.Key,
                g => (IReadOnlyList<string>)g.Select(f => f.ValidationMessage).ToList()
            );
    }

    private ValidationFailures(IReadOnlyDictionary<string, IReadOnlyList<string>> failures)
        : base("validation_failures", "One or more validations failed:")
    {
        Failures = failures;
    }

    /// <summary>
    /// Returns all validation failures as a flat collection of <see cref="ValidationFailure"/> instances.
    /// </summary>
    public IEnumerable<ValidationFailure> ToValidationFailureCollection() =>
        Failures.SelectMany(kvp => kvp.Value.Select(message => new ValidationFailure(kvp.Key, message)));

    /// <summary>
    /// Gets all validation messages for the specified <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The name of the property.</param>
    /// <returns>A collection of messages, or an empty collection if none exist.</returns>
    public IEnumerable<string> GetFailuresFor(string property) =>
        Failures.TryGetValue(property, out var messages)
            ? messages
            : Enumerable.Empty<string>();

    /// <summary>
    /// Returns a new <see cref="ValidationFailures"/> with the given validation failure added.
    /// </summary>
    public ValidationFailures Add(ValidationFailure failure)
    {
        ArgumentNullException.ThrowIfNull(failure);

        return Add(failure.Property, failure.ValidationMessage);
    }

    /// <summary>
    /// Returns a new <see cref="ValidationFailures"/> with the given message added for the specified property.
    /// </summary>
    public ValidationFailures Add(string property, string message)
    {
        ArgumentNullException.ThrowIfNull(property);
        ArgumentNullException.ThrowIfNull(message);

        var newDict = Failures.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToList()
        );

        if (!newDict.TryGetValue(property, out var list))
        {
            list = [];
            newDict[property] = list;
        }

        list.Add(message);

        return new ValidationFailures(ToReadOnly(newDict));
    }

    /// <summary>
    /// Returns a new <see cref="ValidationFailures"/> with the given messages added for the specified property.
    /// </summary>
    public ValidationFailures AddRange(string property, IEnumerable<string> messages)
    {
        ArgumentNullException.ThrowIfNull(property);
        ArgumentNullException.ThrowIfNull(messages);

        var newDict = Failures.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToList()
        );

        if (!newDict.TryGetValue(property, out var list))
        {
            list = [];
            newDict[property] = list;
        }

        list.AddRange(messages);

        return new ValidationFailures(ToReadOnly(newDict));
    }

    /// <summary>
    /// Returns a new <see cref="ValidationFailures"/> that merges this instance with another.
    /// </summary>
    public ValidationFailures Merge(ValidationFailures other)
    {
        ArgumentNullException.ThrowIfNull(other);

        var result = this;
        foreach (var (property, messages) in other.Failures)
        {
            result = result.AddRange(property, messages);
        }

        return result;
    }

    private static Dictionary<string, IReadOnlyList<string>> ToReadOnly(Dictionary<string, List<string>> source) =>
        source.ToDictionary(
            kvp => kvp.Key,
            kvp => (IReadOnlyList<string>)kvp.Value.AsReadOnly()
        );
}