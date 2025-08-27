using System.Text.Json;
using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Notifications.Store.JsonSerialization;
using Toarnbeike.Results.Messaging.Tests.TestData.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.Notifications.Store.JsonConversion;

public class NotificationJsonConverterTests
{
    private readonly JsonSerializerOptions _options;

    public NotificationJsonConverterTests()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new NotificationJsonConverter());
        _options.Converters.Add(new NotificationIdJsonConverter());
        _options.WriteIndented = false;
    }

    [Fact]
    public void Serialize_And_Deserialize_Should_Roundtrip()
    {
        // Arrange
        var notification = new SampleNotification("Hello World");

        // Act
        var json = JsonSerializer.Serialize<INotification>(notification, _options);
        var deserialized = JsonSerializer.Deserialize<INotification>(json, _options);

        // Assert
        deserialized.ShouldBeOfType<SampleNotification>();
        ((SampleNotification)deserialized).Payload.ShouldBe(notification.Payload);
    }

    [Fact]
    public void Deserialize_MissingType_ShouldThrow()
    {
        // Arrange
        var json = "{\"Payload\":\"Hello\"}"; // geen $type

        // Act & Assert
        Should.Throw<JsonException>(() =>
            JsonSerializer.Deserialize<INotification>(json, _options));
    }

    [Fact]
    public void Deserialize_EmptyType_ShouldThrow()
    {
        // Arrange
        var json = "{\"$type\":\"\",\"Payload\":\"Hello\"}";

        // Act & Assert
        Should.Throw<JsonException>(() =>
            JsonSerializer.Deserialize<INotification>(json, _options));
    }

    [Fact]
    public void Deserialize_UnknownType_ShouldThrow()
    {
        // Arrange
        var json = "{\"$type\":\"Unknown.Type, Unknown.Assembly\",\"Payload\":\"Hello\"}";

        // Act & Assert
        Should.Throw<JsonException>(() =>
            JsonSerializer.Deserialize<INotification>(json, _options));
    }
}