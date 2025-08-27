using System.Text.Json;
using System.Text.Json.Serialization;

namespace Toarnbeike.Results.Messaging.Notifications.Store.JsonSerialization;

/// <summary>
/// A <see cref="JsonConverter{T}"/> for serializing and deserializing <see cref="INotification"/> instances.
/// </summary>
/// <remarks>
/// <para>
/// This converter adds a discriminator property (<c>$type</c>) to the serialized JSON, 
/// so that derived <see cref="NotificationBase"/> types can be reconstructed when deserializing.
/// </para>
/// <para>
/// By default, the converter writes {<c>FullName, AssemblyName</c>} of the notification type.
/// This avoids breaking backward compatibility when assemblies are versioned, while
/// still being specific enough to locate the correct type at runtime.
/// </para>
/// <example>
/// Example JSON for a <c>SampleNotification</c>:
/// <code language="json">
/// {
///   "$type": "Toarnbeike.Results.Messaging.Tests.TestData.SampleNotification, Results.Messaging.Tests",
///   "Payload": "Payload",
///   "Id": "0198e536-523b-79e2-9732-f3cdfa633691",
///   "CreatedAt": "2025-08-26T07:09:59.2271163Z",
///   "ProcessingState": {
///     "$type": "NotProcessed"
///   }
/// }
/// </code>
/// </example>
/// </remarks>
internal sealed class NotificationJsonConverter : JsonConverter<INotification>
{
    private const string TypePropertyName = "$type";

    /// <inheritdoc />
    public override INotification Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var json = doc.RootElement.GetRawText();

        if (!doc.RootElement.TryGetProperty(TypePropertyName, out var typeProp))
            throw new JsonException($"Missing {TypePropertyName}");

        var typeName = typeProp.GetString();
        if (string.IsNullOrWhiteSpace(typeName))
            throw new JsonException($"{TypePropertyName} is null or empty");

        // Try to resolve the concrete type:
        // 1. Use Type.GetType() (works if assembly already loaded).
        // 2. Otherwise, search all loaded assemblies for the FullName part.
        var typeNameParts = typeName.Split(',', 2);
        var fullName = typeNameParts[0].Trim();

        var concreteType = Type.GetType(typeName)
                           ?? AppDomain.CurrentDomain
                               .GetAssemblies()
                               .Select(a => a.GetType(fullName))
                               .FirstOrDefault(t => t != null)
                           ?? throw new JsonException($"Type '{typeName}' not found");

        return (INotification)JsonSerializer.Deserialize(json, concreteType, options)!;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, INotification value, JsonSerializerOptions options)
    {
        // Serialize the concrete type into a JsonDocument to enumerate its properties.
        using var jsonDoc = JsonDocument.Parse(JsonSerializer.Serialize(value, value.GetType(), options));

        writer.WriteStartObject();

        // Write the type discriminator.
        writer.WriteString(
            TypePropertyName,
            $"{value.GetType().FullName}, {value.GetType().Assembly.GetName().Name}"
        );

        // Copy the original properties.
        foreach (var prop in jsonDoc.RootElement.EnumerateObject())
        {
            prop.WriteTo(writer);
        }

        writer.WriteEndObject();
    }
}