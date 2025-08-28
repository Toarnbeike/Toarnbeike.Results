namespace Toarnbeike.Results.Messaging.Notifications;

/// <summary>
/// Represents a unique identifier for a notification.
/// </summary>
/// <param name="Value">The underlying <see cref="Guid"/> value of the notification identifier.</param>
public readonly record struct NotificationId(Guid Value);