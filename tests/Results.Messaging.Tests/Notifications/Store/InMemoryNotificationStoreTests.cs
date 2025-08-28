using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Notifications.Store;
using Toarnbeike.Results.Messaging.Tests.TestData.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.Notifications.Store;

public class InMemoryNotificationStoreTests
{
    private readonly InMemoryNotificationStore _store = new();

    [Fact]
    public async Task AddAsync_StoresNotification()
    {
        var notification = new SampleNotification("Message 1");

        await _store.AddAsync(notification);

        var fetched = await _store.GetAsync(notification.Id);
        fetched.ShouldBeSameAs(notification);
    }

    [Fact]
    public async Task GetAsync_ReturnsExactSameReference()
    {
        var notification = new SampleNotification("Message 2");
        await _store.AddAsync(notification);

        var fetched = await _store.GetAsync(notification.Id);

        ReferenceEquals(notification, fetched).ShouldBeTrue();
    }

    [Fact]
    public async Task GetAsync_ThrowsIfNotFound()
    {
        var id = new NotificationId(Guid.NewGuid());

        await Should.ThrowAsync<KeyNotFoundException>(() => _store.GetAsync(id));
    }

    [Fact]
    public async Task UpdateAsync_ReplacesNotification()
    {
        var notification = new SampleNotification("Message 3");
        await _store.AddAsync(notification);

        var updated = new SampleNotification("Updated") { Id = notification.Id };

        await _store.UpdateAsync(updated);
        var fetched = await _store.GetAsync(notification.Id);

        fetched.ShouldBeSameAs(updated);
        fetched.ShouldNotBeSameAs(notification);
    }

    [Fact]
    public async Task GetUnprocessedAsync_ReturnsOnlyUnprocessed()
    {
        var n1 = new SampleNotification("n1");
        var n2 = new SampleNotification("n2");
        var n3 = new SampleNotification("n3");

        await _store.AddAsync(n1);
        await _store.AddAsync(n2);
        await _store.AddAsync(n3);

        await _store.MarkAsHandledAsync(n2.Id, "TestProc", Result.Success());

        var unprocessed = await _store.GetUnprocessedAsync();

        unprocessed.ShouldContain(n1);
        unprocessed.ShouldContain(n3);
        unprocessed.ShouldNotContain(n2);
    }

    [Fact]
    public async Task MarkAsHandledAsync_UpdatesProcessingState()
    {
        var n = new SampleNotification("n4");
        await _store.AddAsync(n);

        await _store.MarkAsHandledAsync(n.Id, "Proc1", Result.Success());

        var fetched = await _store.GetAsync(n.Id);

        fetched.ProcessingState.IsProcessed.ShouldBeTrue();
    }
}