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
        if (value is TextPartInput text)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "text");
            if (!string.IsNullOrEmpty(text.Id))
                writer.WriteString("id", text.Id);
            writer.WriteString("text", text.Text);
            if (text.Synthetic.HasValue)
                writer.WriteBoolean("synthetic", text.Synthetic.Value);
            if (text.Ignored.HasValue)
                writer.WriteBoolean("ignored", text.Ignored.Value);
            if (text.Time is not null)
            {
                writer.WritePropertyName("time");
                writer.WriteStartObject();
                writer.WriteNumber("start", text.Time.Start);
                if (text.Time.End.HasValue)
                    writer.WriteNumber("end", text.Time.End.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
        else if (value is FilePartInput file)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "file");
            if (!string.IsNullOrEmpty(file.Id))
                writer.WriteString("id", file.Id);
            writer.WriteString("mime", file.Mime);
            writer.WriteString("url", file.Url);
            if (!string.IsNullOrEmpty(file.Filename))
                writer.WriteString("filename", file.Filename);
            writer.WriteEndObject();
        }
        else if (value is AgentPartInput agent)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "agent");
            if (!string.IsNullOrEmpty(agent.Id))
                writer.WriteString("id", agent.Id);
            writer.WriteString("name", agent.Name);
            writer.WriteEndObject();
        }
        else if (value is SubtaskPartInput subtask)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "subtask");
            if (!string.IsNullOrEmpty(subtask.Id))
                writer.WriteString("id", subtask.Id);
            writer.WriteString("prompt", subtask.Prompt);
            if (!string.IsNullOrEmpty(subtask.Description))
                writer.WriteString("description", subtask.Description);
            writer.WriteString("agent", subtask.Agent);
            if (subtask.Model is not null)
            {
                writer.WritePropertyName("model");
                writer.WriteStartObject();
                writer.WriteString("providerID", subtask.Model.ProviderId);
                writer.WriteString("modelID", subtask.Model.ModelId);
                writer.WriteEndObject();
            }
            if (!string.IsNullOrEmpty(subtask.Command))
                writer.WriteString("command", subtask.Command);
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