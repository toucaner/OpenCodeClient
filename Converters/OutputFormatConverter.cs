using System.Text.Json;
using System.Text.Json.Serialization;
using OpenCodeClient.Models;

namespace OpenCodeClient.Converters;

public class OutputFormatConverter : JsonConverter<OutputFormat>
{
    public override OutputFormat? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = root.TryGetProperty("type", out var t) ? t.GetString() : null;

        var json = root.GetRawText();

        return type switch
        {
            "text" => JsonSerializer.Deserialize<OutputFormatText>(json, options),
            "json_schema" => JsonSerializer.Deserialize<OutputFormatJsonSchema>(json, options),
            _ => JsonSerializer.Deserialize<OutputFormatText>(json, options)
        };
    }

    public override void Write(Utf8JsonWriter writer, OutputFormat value, JsonSerializerOptions options)
    {
        if (value is OutputFormatText)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "text");
            writer.WriteEndObject();
        }
        else if (value is OutputFormatJsonSchema schema)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "json_schema");
            writer.WriteEndObject();
        }
        else
        {
            writer.WriteStartObject();
            writer.WriteString("type", "text");
            writer.WriteEndObject();
        }
    }
}