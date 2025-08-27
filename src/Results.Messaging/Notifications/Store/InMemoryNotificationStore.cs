using System.Collections.Concurrent;

namespace Toarnbeike.Results.Messaging.Notifications.Store;

/// <summary>
/// A lightweight in-memory implementation of <see cref="INotificationStore"/>.
/// 
/// Notifications are kept in memory only and will be lost when the application exits.
/// This implementation is primarily intended for testing or demonstration purposes.
/// 
/// Unlike persistent stores, it does not serialize notifications to JSON.
/// </summary>
public class InMemoryNotificationStore : NotificationStoreBase
{
    private readonly ConcurrentDictionary<NotificationId, INotification> _store = new();

    /// <inheritdoc />
    public override Task<INotification> GetAsync(NotificationId notificationId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store[notificationId]);
    }

    /// <inheritdoc />
    public override Task<IReadOnlyList<INotification>> GetUnprocessedAsync(CancellationToken cancellationToken = default)
    {
        var result = _store.Values
            .Where(n => !n.ProcessingState.IsProcessed)
            .ToList();

        return Task.FromResult<IReadOnlyList<INotification>>(result);
    }

    /// <inheritdoc />
    public override Task AddAsync(INotification notification, CancellationToken cancellationToken = default)
    {
        _store[notification.Id] = notification;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public override Task UpdateAsync(INotification notification, CancellationToken cancellationToken = default)
    {
        _store.AddOrUpdate(notification.Id, notification, (_, _) => notification);
        return Task.CompletedTask;
    }
}