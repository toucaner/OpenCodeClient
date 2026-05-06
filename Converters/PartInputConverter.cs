using System.Text.Json;
using System.Text.Json.Serialization;
using OpenCodeClient.Models;

namespace OpenCodeClient.Converters;

public class PartInputConverter : JsonConverter<PartInput>
{
    private static readonly JsonSerializerOptions DirectOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public override PartInput? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = root.TryGetProperty("type", out var t) ? t.GetString() : null;
        var json = root.GetRawText();

        return type switch
        {
            "text" => JsonSerializer.Deserialize<TextPartInput>(json, DirectOptions),
            "file" => JsonSerializer.Deserialize<FilePartInput>(json, DirectOptions),
            "agent" => JsonSerializer.Deserialize<AgentPartInput>(json, DirectOptions),
            "subtask" => JsonSerializer.Deserialize<SubtaskPartInput>(json, DirectOptions),
            _ => JsonSerializer.Deserialize<TextPartInput>(json, DirectOptions)
        };
    }

    public override void Write(Utf8JsonWriter writer, PartInput value, JsonSerializerOptions options)
    {
        var runtimeType = value.GetType();

        if (runtimeType == typeof(TextPartInput))
            JsonSerializer.Serialize(writer, (TextPartInput)value, DirectOptions);
        else if (runtimeType == typeof(FilePartInput))
            JsonSerializer.Serialize(writer, (FilePartInput)value, DirectOptions);
        else if (runtimeType == typeof(AgentPartInput))
            JsonSerializer.Serialize(writer, (AgentPartInput)value, DirectOptions);
        else if (runtimeType == typeof(SubtaskPartInput))
            JsonSerializer.Serialize(writer, (SubtaskPartInput)value, DirectOptions);
        else
            JsonSerializer.Serialize(writer, (TextPartInput)value, DirectOptions);
    }
}