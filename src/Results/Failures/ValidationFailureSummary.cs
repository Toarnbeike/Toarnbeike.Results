namespace Toarnbeike.Results.Failures;

/// <summary>
/// Represents a validation failure that combines multiple <see cref="ValidationFailure"/> instances
/// into a single <see cref="Failure"/>.
/// </summary>
/// <remarks>
/// Stores validation messages grouped by property name. 
/// Use <see cref="Failures"/> to retrieve all grouped failures, or <see cref="GetFailuresFor(string)"/> to retrieve messages for a specific property.
/// </remarks>
public sealed record ValidationFailureSummary : Failure
{
    /// <summary>
    /// Gets all validation failures grouped by property name.
    /// </summary>
    public IDictionary<string, string[]> Failures { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFailureSummary"/> class with a collection of failures.
    /// </summary>
    /// <param name="failures">The collection of validation failures to include.</param>
    /// <param name="message">Optional message specific to this validation summary. </param>
    /// <exception cref="ArgumentException">Thrown if the collection is empty or contains null entries.</exception>
    public ValidationFailureSummary(IEnumerable<ValidationFailure> failures, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(failures);

        var list = failures.ToList();
        if (list.Count == 0)
        {
            throw new ArgumentException("At least one validation failure must be provided.", nameof(failures));
        }

        Failures = list
            .GroupBy(f => f.Property)
            .ToDictionary(
                g => g.Key,
                g => g.Select(f => f.ValidationMessage).ToArray()
            );

        Message = message ?? "One or more validation failures occured.";
        Category = FailureCategory.Validation;
    }

    /// <summary>
    /// Gets all validation messages for the specified <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The name of the property.</param>
    /// <returns>A collection of messages, or an empty collection if none exist.</returns>
    public IEnumerable<string> GetFailuresFor(string property) =>
        Failures.TryGetValue(property, out var messages)
            ? messages
            : Enumerable.Empty<string>();
}