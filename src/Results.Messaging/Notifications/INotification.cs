namespace Toarnbeike.Results.Messaging.Notifications;

/// <summary>
/// Represents a notification that can be published within the system.
/// Notifications are immutable records containing tracing and processing information.
/// </summary>
/// <remarks> When creating a new notification type, use <see cref="NotificationBase"/> as base.</remarks>
public interface INotification
{
    /// <summary>
    /// Gets the unique identifier of the notification.
    /// </summary>
    NotificationId Id { get; }

    /// <summary>
    /// Gets the timestamp indicating when the notification was created (UTC).
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the logical sender of this notification, e.g. the bounded context or service.
    /// </summary>
    string Sender { get; }

    /// <summary>
    /// Gets the processing state of the notification.
    /// </summary>
    ProcessingState ProcessingState { get; }

    /// <summary>
    /// Marks this notification as processed with the specified <paramref name="result"/>.<br/>
    /// The processing state will be <see cref="Notifications.ProcessingState.ProcessedSuccessfully"/>
    /// if the result is a success, or <see cref="Notifications.ProcessingState.ProcessedWithFailure"/>
    /// if the result is a failure.
    /// </summary>
    /// <param name="processor">The name of the processor that processed the notification.</param>
    /// <param name="result">The result of processing this notification.</param>
    /// <returns>A new notification instance with updated processing information.</returns>
    internal NotificationBase MarkProcessed(string processor, Result result);
}