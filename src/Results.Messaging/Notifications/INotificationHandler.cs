namespace Toarnbeike.Results.Messaging.Notifications;

/// <summary>
/// Handler for an <see cref="INotification"/>
/// </summary>
/// <remarks>Every notification can have zero, one or more handlers.</remarks>
/// <typeparam name="TNotification">The type of notification this handler listens for.</typeparam>
public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken = default);
}