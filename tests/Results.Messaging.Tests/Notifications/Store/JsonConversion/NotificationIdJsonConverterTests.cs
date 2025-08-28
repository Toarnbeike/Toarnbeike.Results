using System.Text.Json;
using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Notifications.Store.JsonSerialization;

namespace Toarnbeike.Results.Messaging.Tests.Notifications.Store.JsonConversion;
public class NotificationIdJsonConverterTests
{
    private readonly JsonSerializerOptions _options;

    public NotificationIdJsonConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new NotificationIdJsonConverter());
    }

    [Fact]
    public void Serialize_And_Deserialize_Should_Roundtrip()
    {
        var id = new NotificationId(Guid.NewGuid());

        var json = JsonSerializer.Serialize(id, _options);
        var deserialized = JsonSerializer.Deserialize<NotificationId>(json, _options);

        deserialized.Value.ShouldBe(id.Value);
    }

    [Fact]
    public void Deserialize_InvalidGuid_ShouldThrow()
    {
        var invalidJson = "\"not-a-guid\"";

        Should.Throw<JsonException>(() =>
            JsonSerializer.Deserialize<NotificationId>(invalidJson, _options));
    }
}