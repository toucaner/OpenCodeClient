using System.Text.Json;
using System.Text.Json.Serialization;
using OpenCodeClient.Models;

namespace OpenCodeClient.Converters;

public class ToolStateConverter : JsonConverter<ToolState>
{
    public override ToolState? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var status = root.TryGetProperty("status", out var s) ? s.GetString() : null;

        var json = root.GetRawText();

        return status switch
        {
            "pending" => JsonSerializer.Deserialize<ToolStatePending>(json, options),
            "running" => JsonSerializer.Deserialize<ToolStateRunning>(json, options),
            "completed" => JsonSerializer.Deserialize<ToolStateCompleted>(json, options),
            "error" => JsonSerializer.Deserialize<ToolStateError>(json, options),
            _ => JsonSerializer.Deserialize<ToolStatePending>(json, options)
        };
    }

    public override void Write(Utf8JsonWriter writer, ToolState value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(value, value.GetType(), options);
        using var doc = JsonDocument.Parse(json);
        doc.RootElement.WriteTo(writer);
    }
}