namespace Toarnbeike.Results.Failures;

/// <summary>
/// Represents a failure that aggregates multiple individual failures.
/// </summary>
/// <remarks>This class is used to encapsulate multiple failures into a single failure object,  allowing them to
/// be treated as a group. It provides access to the collection of  individual failures that contributed to the
/// aggregate failure.</remarks>
public sealed record AggregateFailure : Failure
{
    /// <summary>
    /// Gets the collection of individual failures that contributed to this aggregate failure.
    /// </summary>
    public IReadOnlyCollection<Failure> Failures { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateFailure"/> class with a collection of failures.
    /// Nested <see cref="AggregateFailure"/> instances are flattened automatically.
    /// </summary>
    /// <param name="failures">The collection of failures that caused the aggregate failure.</param>
    public AggregateFailure(IEnumerable<Failure> failures)
        : base("aggregate", "Multiple failures occurred")
    {
        ArgumentNullException.ThrowIfNull(failures);

        var flattened = Flatten(failures).ToList();

        if (flattened.Count == 0)
        {
            throw new ArgumentException("At least one failure must be provided.", nameof(failures));
        }

        if (flattened.Any(f => f is null))
        {
            throw new ArgumentException("Failures collection may not contain null elements.", nameof(failures));
        }

        Failures = flattened.AsReadOnly();
    }

    /// <summary>
    /// Adds a new failure to the collection and returns an updated <see cref="AggregateFailure"/> instance.
    /// </summary>
    /// <param name="failure">The failure to add. Cannot be <see langword="null"/>.</param>
    /// <returns>A new <see cref="AggregateFailure"/> instance containing the added failure.</returns>
    public AggregateFailure Add(Failure failure)
    {
        ArgumentNullException.ThrowIfNull(failure);
        return new AggregateFailure(Failures.Append(failure));
    }

    /// <summary>
    /// Combines the current <see cref="AggregateFailure"/> instance with another, producing a new instance that
    /// aggregates the failures from both.
    /// </summary>
    /// <param name="other">The <see cref="AggregateFailure"/> instance to combine with the current instance. Cannot be <see
    /// langword="null"/>.</param>
    /// <returns>A new <see cref="AggregateFailure"/> instance containing the combined failures from both instances. If the
    /// <paramref name="other"/> instance has no failures, the current instance is returned.</returns>
    public AggregateFailure Combine(AggregateFailure other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return Add(other);
    }

    /// <summary>
    /// Ensures that the collection is flat, i.e. no AggregateFailures are part of the inner collection of failures,
    /// their inner failures are used instead.
    /// </summary>
    private static IEnumerable<Failure> Flatten(IEnumerable<Failure> failures)
    {
        foreach (var failure in failures)
        {
            if (failure is AggregateFailure aggregate)
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