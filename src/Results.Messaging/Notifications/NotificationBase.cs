namespace Toarnbeike.Results.Messaging.Notifications;

/// <summary>
/// Base record for implementing notifications with default tracing information.
/// </summary>
/// <param name="Id">The unique identifier of the notification.</param>
/// <param name="CreatedAt">The timestamp indicating when the notification was created (UTC).</param>
/// <param name="Sender">The logical sender of the notification, e.g. the bounded context or service.</param>
/// <param name="ProcessingState">The current processing state of the notification.</param>
public abstract record NotificationBase(NotificationId Id, DateTime CreatedAt, string Sender, ProcessingState ProcessingState) : INotification
{
    /// <summary>
    /// Initializes a new <see cref="NotificationBase"/> with a new <see cref="Guid"/> and current UTC creation time.
    /// </summary>
    /// <param name="sender">The logical sender of this notification.</param>
    protected NotificationBase(string sender)
        : this(new NotificationId(Guid.CreateVersion7()), DateTime.UtcNow, sender, ProcessingState.NotProcessed()) { }

    /// <inheritdoc />
    public NotificationBase MarkProcessed(string processor, Result result)
    {
        var newState = ProcessingState.FromResult(DateTime.UtcNow, processor, result);
        return this with { ProcessingState = newState };
    }
}