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
    private readonly Dictionary<string, List<string>> _failures = [];

    public ValidationFailures(params IEnumerable<ValidationFailure> failures) 
        : base("validation_failures", "One or more validations failed:")
    {
        _failures = failures
            .GroupBy(f => f.Property)
            .ToDictionary(
                g => g.Key,
                g => g.Select(f => f.ValidationMessage).ToList());
    }

    /// <summary>
    /// Gets all validation failures grouped by property name.
    /// </summary>
    public IReadOnlyDictionary<string, IReadOnlyList<string>> Failures =>
        _failures.ToDictionary(
            kvp => kvp.Key,
            kvp => (IReadOnlyList<string>)kvp.Value);

    /// <summary>
    /// Adds a new validation failure for the specified <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The name of the property that failed validation.</param>
    /// <param name="message">The validation message associated with the failure.</param>
    public void Add(string property, string message)
    {
        if (!_failures.TryGetValue(property, out var messages))
        {
            messages = [];
            _failures[property] = messages;
        }

        messages.Add(message);
    }

    /// <summary>
    /// Adds a new validation failure.
    /// </summary>
    /// <param name="validationFailure">The validation failure to add.</param>
    public void Add(ValidationFailure validationFailure) =>
        Add(validationFailure.Property, validationFailure.ValidationMessage);

    /// <summary>
    /// Adds multiple validation messages for the specified <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The property name.</param>
    /// <param name="messages">The messages to add.</param>
    public void AddRange(string property, IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            Add(property, message);
        }
    }

    /// <summary>
    /// Merges another <see cref="ValidationFailures"/> instance into the current one.
    /// </summary>
    /// <param name="other">The other validation failures to merge.</param>
    public void Merge(ValidationFailures other)
    {
        foreach (var (property, messages) in other._failures)
        {
            AddRange(property, messages);
        }
    }

    /// <summary>
    /// Returns all validation failures as a flat collection of <see cref="ValidationFailure"/> instances.
    /// </summary>
    public IEnumerable<ValidationFailure> ToValidationFailureCollection() =>
        _failures.SelectMany(
            kvp => kvp.Value.Select(
                message => new ValidationFailure(kvp.Key, message)));

    /// <summary>
    /// Gets all validation messages for the specified <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The name of the property.</param>
    /// <returns>A collection of messages, or an empty collection if none exist.</returns>
    public IEnumerable<string> GetFailuresFor(string property) =>
        _failures.TryGetValue(property, out var messages)
            ? messages
            : Enumerable.Empty<string>();
}