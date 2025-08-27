using System.Text.Json;
using System.Text.Json.Serialization;

namespace Toarnbeike.Results.Messaging.Notifications.Store.JsonSerialization;

/// <summary>
/// Small JSON converter for <see cref="NotificationId"/> to avoid nested Value property.
/// </summary>
internal class NotificationIdJsonConverter : JsonConverter<NotificationId>
{
    /// <inheritdoc />
    public override NotificationId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new NotificationId(guid);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, NotificationId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}