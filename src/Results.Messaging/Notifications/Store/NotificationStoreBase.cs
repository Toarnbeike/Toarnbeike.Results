using System.Text.Json;
using Toarnbeike.Results.Messaging.Notifications.Store.JsonSerialization;

namespace Toarnbeike.Results.Messaging.Notifications.Store;

/// <summary>
/// Provides a base class for <see cref="INotificationStore"/> implementations.
/// 
/// This class can be extended by users to implement custom storage strategies,
/// for example persisting notifications in a database or a file system.
/// 
/// It contains helper methods for JSON serialization/deserialization
/// to ensure consistency across implementations.
/// </summary>
public abstract class NotificationStoreBase : INotificationStore
{
    private readonly JsonSerializerOptions _options;

    protected NotificationStoreBase()
    {
        _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        _options.Converters.Add(new NotificationJsonConverter());
        _options.Converters.Add(new NotificationIdJsonConverter());
    }

    protected string Serialize(INotification notification)
        => JsonSerializer.Serialize(notification, _options);

    protected INotification Deserialize(string json)
        => JsonSerializer.Deserialize<INotification>(json, _options)!;

    /// <inheritdoc />
    public abstract Task AddAsync(INotification notification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing notification in the store.
    /// </summary>
    /// <param name="notification">The notification to update.</param>
    /// <param name="cancellationToken">To cancel the asynchronous operation.</param>
    public abstract Task UpdateAsync(INotification notification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a notification by its unique identifier.
    /// </summary>
    /// <param name="notificationId">The unique identifier of the notification to retrieve.</param>
    /// <param name="cancellationToken">To cancel the asynchronous operation.</param>
    /// <returns>
    /// The <see cref="INotification"/> associated with the specified <paramref name="notificationId"/>.
    /// </returns>
    public abstract Task<INotification> GetAsync(NotificationId notificationId, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public abstract Task<IReadOnlyList<INotification>> GetUnprocessedAsync(
        CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public async Task MarkAsHandledAsync(NotificationId notificationId, string processor, Result processingResult, CancellationToken cancellationToken = default)
    {
        var notification = await GetAsync(notificationId, cancellationToken).ConfigureAwait(false);
        var updated = notification.MarkProcessed(processor, processingResult);
        await UpdateAsync(updated, cancellationToken).ConfigureAwait(false);
    }
}