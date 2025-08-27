namespace Toarnbeike.Results.Messaging.Notifications.Store;

/// <summary>
/// Defines a contract for storing and retrieving <see cref="INotification"/> instances.
/// A notification store is responsible for persisting notifications,
/// retrieving unprocessed notifications, and marking them as processed
/// once they have been handled.
/// </summary>
/// <remarks>
/// When creating a custom implementation of an <see cref="INotificationStore"/>,
/// inherit from <see cref="NotificationStoreBase"/> to leverage build in functionality.
/// </remarks>
public interface INotificationStore
{
    /// <summary>
    /// Persists a notification into the store.
    /// </summary>
    /// <param name="notification">The notification to store.</param>
    /// <param name="cancellationToken">To cancel the asynchronous operation.</param>
    Task AddAsync(INotification notification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all notifications that are not yet processed.
    /// </summary>
    /// <param name="cancellationToken">To cancel the asynchronous operation.</param>
    Task<IReadOnlyList<INotification>> GetUnprocessedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks the notification with the given <paramref name="notificationId"/> as processed.
    /// </summary>
    /// <param name="notificationId">The id of the notification that was processed.</param>
    /// <param name="processor">The name of the processor.</param>
    /// <param name="processingResult">The result of the processing.</param>
    /// <param name="cancellationToken">To cancel the asynchronous operation.</param>
    Task MarkAsHandledAsync(NotificationId notificationId, string processor, Result processingResult, CancellationToken cancellationToken = default);
}