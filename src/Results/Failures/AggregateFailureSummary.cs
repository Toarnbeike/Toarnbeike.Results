namespace Toarnbeike.Results.Failures;

/// <summary>
/// Represents a failure that summarizes multiple individual failures.
/// </summary>
/// <remarks>
/// This class is used to encapsulate multiple failures into a single failure object,  allowing them to
/// be treated as a group. It provides access to the collection of individual failures that contributed to the
/// aggregate failure.</remarks>
public sealed record AggregateFailureSummary : Failure
{
    /// <summary>
    /// Gets the collection of individual failures that contributed to this aggregate failure.
    /// </summary>
    public IReadOnlyCollection<Failure> Failures { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateFailureSummary"/> class with a collection of failures.
    /// Nested <see cref="AggregateFailureSummary"/> instances are flattened automatically.
    /// </summary>
    /// <param name="failures">The collection of failures that caused the aggregate failure.</param>
    /// <param name="message">Optionally the message specific for this summary. </param>
    public AggregateFailureSummary(IEnumerable<Failure> failures, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(failures);

        var flattened = Flatten(failures).ToList();

        if (flattened.Count == 0)
        {
            throw new ArgumentException("At least one failure must be provided.", nameof(failures));
        }

        Failures = flattened.AsReadOnly();
        Message = message ?? "Multiple failures occurred";
        Category = Failures.Max(x => x.Category);
    }

    /// <summary>
    /// Ensures that the collection is flat, i.e. no AggregateFailures are part of the inner collection of failures,
    /// their inner failures are used instead.
    /// </summary>
    private static IEnumerable<Failure> Flatten(IEnumerable<Failure> failures)
    {
        foreach (var failure in failures)
        {
            if (failure is AggregateFailureSummary aggregate)
            {
                foreach (var inner in aggregate.Failures)
                {
                    yield return inner;
                }
            }
            else
            {
                yield return failure;
            }
        }
    }
}