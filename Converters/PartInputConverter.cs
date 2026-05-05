using System.Text.Json;
using System.Text.Json.Serialization;
using OpenCodeClient.Models;

namespace OpenCodeClient.Converters;

public class PartInputConverter : JsonConverter<PartInput>
{
    public override PartInput? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = root.TryGetProperty("type", out var t) ? t.GetString() : null;

        var json = root.GetRawText();

        return type switch
        {
            "text" => JsonSerializer.Deserialize<TextPartInput>(json, options),
            "file" => JsonSerializer.Deserialize<FilePartInput>(json, options),
            "agent" => JsonSerializer.Deserialize<AgentPartInput>(json, options),
            "subtask" => JsonSerializer.Deserialize<SubtaskPartInput>(json, options),
            _ => JsonSerializer.Deserialize<TextPartInput>(json, options)
        };
    }

    public override void Write(Utf8JsonWriter writer, PartInput value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}