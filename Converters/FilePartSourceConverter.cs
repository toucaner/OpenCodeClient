using System.Text.Json;
using System.Text.Json.Serialization;
using OpenCodeClient.Models;

namespace OpenCodeClient.Converters;

public class FilePartSourceConverter : JsonConverter<FilePartSource>
{
    public override FilePartSource? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = root.TryGetProperty("type", out var t) ? t.GetString() : null;

        var json = root.GetRawText();

        return type switch
        {
            "file" => JsonSerializer.Deserialize<FileSource>(json, options),
            "symbol" => JsonSerializer.Deserialize<SymbolSource>(json, options),
            "resource" => JsonSerializer.Deserialize<ResourceSource>(json, options),
            _ => JsonSerializer.Deserialize<FileSource>(json, options)
        };
    }

    public override void Write(Utf8JsonWriter writer, FilePartSource value, JsonSerializerOptions options)
    {
        if (value is FileSource file)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "file");
            writer.WriteString("path", file.Path);
            writer.WriteEndObject();
        }
        else if (value is SymbolSource symbol)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "symbol");
            writer.WriteString("path", symbol.Path);
            writer.WriteEndObject();
        }
        else if (value is ResourceSource resource)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "resource");
            writer.WriteString("uri", resource.Uri);
            writer.WriteEndObject();
        }
        else
        {
            writer.WriteStartObject();
            writer.WriteString("type", "file");
            writer.WriteEndObject();
        }
    }
}