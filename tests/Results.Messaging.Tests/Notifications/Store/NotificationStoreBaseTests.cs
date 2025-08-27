using System.Text.Json;
using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Notifications.Store;
using Toarnbeike.Results.Messaging.Tests.TestData.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.Notifications.Store;
public class NotificationStoreBaseTests
{
    private class TestNotificationStore : NotificationStoreBase
    {
        public string SerializePublic(INotification notification) => Serialize(notification);
        public INotification DeserializePublic(string json) => Deserialize(json);

        public override Task AddAsync(INotification notification, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
        public override Task UpdateAsync(INotification notification, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
        public override Task<INotification> GetAsync(NotificationId notificationId, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
        public override Task<IReadOnlyList<INotification>> GetUnprocessedAsync(CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }

    private readonly TestNotificationStore _store = new();

    [Fact]
    public void SerializeDeserialize_Roundtrip_Works()
    {
        var notification = new SampleNotification("Hello World");

        var json = _store.SerializePublic(notification);
        var deserialized = _store.DeserializePublic(json);

        deserialized.ShouldBeOfType<SampleNotification>();
        deserialized.Id.ShouldBe(notification.Id);
        deserialized.CreatedAt.ShouldBe(notification.CreatedAt);
        ((SampleNotification)deserialized).Payload.ShouldBe("Hello World");
    }

    [Fact]
    public void Serialize_IncludesTypeDiscriminator()
    {
        var notification = new SampleNotification("Test");

        var json = _store.SerializePublic(notification);

        json.ShouldContain("\"$type\":");
        json.ShouldContain("SampleNotification");
    }

    [Fact]
    public void Deserialize_ExtraProperties_Ignored()
    {
        var json = """
            {
              "$type": "Toarnbeike.Results.Messaging.Tests.TestData.Notifications.SampleNotification, Results.Messaging.Tests",
              "Payload": "Backwards compat",
              "Id": "0198e536-523b-79e2-9732-f3cdfa633691",
              "CreatedAt": "2025-08-26T07:09:59.2271163Z",
              "ProcessingState": { "$type": "NotProcessed" },
              "Unexpected": "Should be ignored"
            }
            """;

        var deserialized = _store.DeserializePublic(json);

        var sample = deserialized.ShouldBeOfType<SampleNotification>();
        sample.Payload.ShouldBe("Backwards compat");
    }

    [Fact]
    public void Deserialize_MissingType_ThrowsJsonException()
    {
        var json = """
            {
              "Payload": "No type info"
            }
            """;

        Should.Throw<JsonException>(() => _store.DeserializePublic(json));
    }

    [Fact]
    public void Deserialize_EmptyType_ThrowsJsonException()
    {
        var json = """
            {
              "$type": "",
              "Payload": "Empty type"
            }
            """;

        Should.Throw<JsonException>(() => _store.DeserializePublic(json));
    }

    [Fact]
    public void Deserialize_UnknownType_ThrowsJsonException()
    {
        var json = """
            {
              "$type": "NonexistentNamespace.UnknownNotification, UnknownAssembly",
              "Payload": "Bad type"
            }
            """;

        Should.Throw<JsonException>(() => _store.DeserializePublic(json));
    }
}