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
        if (value is ToolStatePending pending)
        {
            writer.WriteStartObject();
            writer.WriteString("status", "pending");
            writer.WriteString("raw", pending.Raw);
            writer.WriteEndObject();
        }
        else if (value is ToolStateRunning running)
        {
            writer.WriteStartObject();
            writer.WriteString("status", "running");
            if (!string.IsNullOrEmpty(running.Title))
                writer.WriteString("title", running.Title);
            writer.WriteEndObject();
        }
        else if (value is ToolStateCompleted completed)
        {
            writer.WriteStartObject();
            writer.WriteString("status", "completed");
            writer.WriteString("output", completed.Output);
            writer.WriteString("title", completed.Title);
            writer.WriteEndObject();
        }
        else if (value is ToolStateError error)
        {
            writer.WriteStartObject();
            writer.WriteString("status", "error");
            writer.WriteString("error", error.Error);
            writer.WriteEndObject();
        }
        else
        {
            writer.WriteStartObject();
            writer.WriteString("status", "pending");
            writer.WriteEndObject();
        }
    }
}