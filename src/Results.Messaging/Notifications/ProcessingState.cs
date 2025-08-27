using System.Text.Json.Serialization;

namespace Toarnbeike.Results.Messaging.Notifications;

/// <summary>
/// Represents the processing state of a notification.
/// Implemented as a discriminated union with three possible cases:
/// <list type="bullet">
/// <item><description><see cref="NotProcessedState"/> — the notification has not yet been processed.</description></item>
/// <item><description><see cref="ProcessedSuccessfullyState"/> — the notification was processed successfully.</description></item>
/// <item><description><see cref="ProcessedWithFailureState"/> — the notification processing failed.</description></item>
/// </list>
/// </summary>
[JsonDerivedType(typeof(NotProcessedState), "NotProcessed")]
[JsonDerivedType(typeof(ProcessedSuccessfullyState), "ProcessedSuccessfully")]
[JsonDerivedType(typeof(ProcessedWithFailureState), "ProcessedWithFailure")]
public abstract record ProcessingState
{
    /// <summary>
    /// Indicates whether the notification was processed.
    /// </summary>
    public abstract bool IsProcessed { get; }

    /// <summary>
    /// Indicates whether the notification was processed successfully.
    /// </summary>
    public abstract bool IsSuccess { get; }

    /// <summary>
    /// Represents a notification that has not yet been processed.
    /// </summary>
    public sealed record NotProcessedState : ProcessingState
    {
        public override bool IsProcessed => false;
        public override bool IsSuccess => false;
    }

    /// <summary>
    /// Represents a notification that has been processed successfully.
    /// </summary>
    /// <param name="ProcessedAt">The date and time the notification was processed.</param>
    /// <param name="ProcessedBy">The name of the processor that processed the notification.</param>
    public sealed record ProcessedSuccessfullyState(DateTime ProcessedAt, string ProcessedBy) : ProcessingState
    {
        public override bool IsProcessed => true;
        public override bool IsSuccess => true;
    }

    /// <summary>
    /// Represents a notification that has been processed but failed.
    /// </summary>
    /// <param name="ProcessedAt">The date and time the notification was processed.</param>
    /// <param name="ProcessedBy">The name of the processor that processed the notification.</param>
    /// <param name="Failure">The reason the processing failed.</param>
    public sealed record ProcessedWithFailureState(DateTime ProcessedAt, string ProcessedBy, Failure Failure)
        : ProcessingState
    {
        public override bool IsProcessed => true;
        public override bool IsSuccess => false;
    }

    /// <summary>
    /// Creates a new <see cref="NotProcessed"/> instance.
    /// </summary>
    public static NotProcessedState NotProcessed() => new();

    /// <summary>
    /// Creates a new <see cref="ProcessedSuccessfully"/> instance.
    /// </summary>
    /// <param name="processedAt">The date and time the notification was processed.</param>
    /// <param name="processedBy">The name of the processor that processed the notification.</param>
    public static ProcessedSuccessfullyState ProcessedSuccessfully(DateTime processedAt, string processedBy) => new(processedAt, processedBy);

    /// <summary>
    /// Creates a new <see cref="ProcessedWithFailure"/> instance.
    /// </summary>
    /// <param name="processedAt">The date and time the notification was processed.</param>
    /// <param name="processedBy">The name of the processor that processed the notification.</param>
    /// <param name="failure">The reason the processing failed.</param>
    public static ProcessedWithFailureState ProcessedWithFailure(DateTime processedAt, string processedBy, Failure failure) => new(processedAt, processedBy, failure);

    /// <summary>
    /// Creates a <see cref="ProcessingState"/> from a <see cref="Result"/>,
    /// mapping success to <see cref="ProcessedSuccessfully"/> and failure to <see cref="ProcessedWithFailure"/>.
    /// </summary>
    /// <param name="processedAt">The date and time the notification was processed.</param>
    /// <param name="processedBy">The name of the processor that processed the notification.</param>
    /// <param name="result">The result of the processing.</param>
    public static ProcessingState FromResult(DateTime processedAt, string processedBy, Result result) =>
        result.TryGetFailure(out var failure)
            ? ProcessedWithFailure(processedAt, processedBy, failure)
            : ProcessedSuccessfully(processedAt, processedBy);
}