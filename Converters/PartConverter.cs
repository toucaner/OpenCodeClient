using System.Text.Json;
using System.Text.Json.Serialization;
using OpenCodeClient.Models;

namespace OpenCodeClient.Converters;

public class PartConverter : JsonConverter<Part>
{
    public override Part? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = root.TryGetProperty("type", out var t) ? t.GetString() : null;

        var json = root.GetRawText();

        return type switch
        {
            "text" => JsonSerializer.Deserialize<TextPart>(json, options),
            "subtask" => JsonSerializer.Deserialize<SubtaskPart>(json, options),
            "reasoning" => JsonSerializer.Deserialize<ReasoningPart>(json, options),
            "file" => JsonSerializer.Deserialize<FilePart>(json, options),
            "tool" => JsonSerializer.Deserialize<ToolPart>(json, options),
            "step-start" => JsonSerializer.Deserialize<StepStartPart>(json, options),
            "step-finish" => JsonSerializer.Deserialize<StepFinishPart>(json, options),
            "snapshot" => JsonSerializer.Deserialize<SnapshotPart>(json, options),
            "patch" => JsonSerializer.Deserialize<PatchPart>(json, options),
            "agent" => JsonSerializer.Deserialize<AgentPart>(json, options),
            "retry" => JsonSerializer.Deserialize<RetryPart>(json, options),
            "compaction" => JsonSerializer.Deserialize<CompactionPart>(json, options),
            _ => JsonSerializer.Deserialize<TextPart>(json, options)
        };
    }

    public override void Write(Utf8JsonWriter writer, Part value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(value, value.GetType(), options);
        using var doc = JsonDocument.Parse(json);
        doc.RootElement.WriteTo(writer);
    }
}