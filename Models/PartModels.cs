using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>Base class for output format variants.</summary>
public abstract class OutputFormat
{
    [JsonPropertyName("type")] public abstract string Type { get; }
}

/// <summary>Plain text output format.</summary>
public class OutputFormatText : OutputFormat
{
    public override string Type => "text";
}

/// <summary>JSON schema output format.</summary>
public class OutputFormatJsonSchema : OutputFormat
{
    public override string Type => "json_schema";
    [JsonPropertyName("schema")] public Dictionary<string, JsonElement> Schema { get; set; } = new();
    [JsonPropertyName("retryCount")] public int? RetryCount { get; set; }
}

/// <summary>Base class for file part source variants.</summary>
public abstract class FilePartSource
{
    [JsonPropertyName("type")] public abstract string Type { get; }
}

/// <summary>Text content with byte range.</summary>
public class FilePartSourceText
{
    [JsonPropertyName("value")] public string Value { get; set; } = "";
    [JsonPropertyName("start")] public long Start { get; set; }
    [JsonPropertyName("end")] public long End { get; set; }
}

/// <summary>File-based source reference.</summary>
public class FileSource : FilePartSource
{
    public override string Type => "file";
    [JsonPropertyName("text")] public FilePartSourceText Text { get; set; } = new();
    [JsonPropertyName("path")] public string Path { get; set; } = "";
}

/// <summary>Symbol-based source reference.</summary>
public class SymbolSource : FilePartSource
{
    public override string Type => "symbol";
    [JsonPropertyName("text")] public FilePartSourceText Text { get; set; } = new();
    [JsonPropertyName("path")] public string Path { get; set; } = "";
    [JsonPropertyName("range")] public Range Range { get; set; } = new();
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("kind")] public int Kind { get; set; }
}

/// <summary>MCP resource-based source reference.</summary>
public class ResourceSource : FilePartSource
{
    public override string Type => "resource";
    [JsonPropertyName("text")] public FilePartSourceText Text { get; set; } = new();
    [JsonPropertyName("clientName")] public string ClientName { get; set; } = "";
    [JsonPropertyName("uri")] public string Uri { get; set; } = "";
}

/// <summary>Base class for message part variants.</summary>
public abstract class Part
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
    [JsonPropertyName("type")] public abstract string Type { get; }
}

/// <summary>A text content part.</summary>
public class TextPart : Part
{
    public override string Type => "text";
    [JsonPropertyName("text")] public string Text { get; set; } = "";
    [JsonPropertyName("synthetic")] public bool? Synthetic { get; set; }
    [JsonPropertyName("ignored")] public bool? Ignored { get; set; }
    [JsonPropertyName("time")] public PartTimeRange? Time { get; set; }
    [JsonPropertyName("metadata")] public Dictionary<string, JsonElement>? Metadata { get; set; }
}

/// <summary>A subtask delegation part.</summary>
public class SubtaskPart : Part
{
    public override string Type => "subtask";
    [JsonPropertyName("prompt")] public string Prompt { get; set; } = "";
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("agent")] public string Agent { get; set; } = "";
    [JsonPropertyName("model")] public SubtaskModel? Model { get; set; }
    [JsonPropertyName("command")] public string? Command { get; set; }
}

/// <summary>Model reference for a subtask.</summary>
public class SubtaskModel
{
    [JsonPropertyName("providerID")] public string ProviderId { get; set; } = "";
    [JsonPropertyName("modelID")] public string ModelId { get; set; } = "";
}

/// <summary>An AI reasoning/thinking part.</summary>
public class ReasoningPart : Part
{
    public override string Type => "reasoning";
    [JsonPropertyName("text")] public string Text { get; set; } = "";
    [JsonPropertyName("time")] public PartTimeRange Time { get; set; } = new();
    [JsonPropertyName("metadata")] public Dictionary<string, JsonElement>? Metadata { get; set; }
}

/// <summary>A file attachment part.</summary>
public class FilePart : Part
{
    public override string Type => "file";
    [JsonPropertyName("mime")] public string Mime { get; set; } = "";
    [JsonPropertyName("url")] public string Url { get; set; } = "";
    [JsonPropertyName("filename")] public string? Filename { get; set; }
    [JsonPropertyName("source")] public FilePartSource? Source { get; set; }
}

/// <summary>A tool invocation part.</summary>
public class ToolPart : Part
{
    public override string Type => "tool";
    [JsonPropertyName("callID")] public string CallId { get; set; } = "";
    [JsonPropertyName("tool")] public string Tool { get; set; } = "";
    [JsonPropertyName("state")] public ToolState State { get; set; } = null!;
    [JsonPropertyName("metadata")] public Dictionary<string, JsonElement>? Metadata { get; set; }
}

/// <summary>Marks the start of a processing step.</summary>
public class StepStartPart : Part
{
    public override string Type => "step-start";
    [JsonPropertyName("snapshot")] public string? Snapshot { get; set; }
}

/// <summary>Marks the end of a processing step with cost and token data.</summary>
public class StepFinishPart : Part
{
    public override string Type => "step-finish";
    [JsonPropertyName("reason")] public string Reason { get; set; } = "";
    [JsonPropertyName("cost")] public double Cost { get; set; }
    [JsonPropertyName("tokens")] public TokenUsage Tokens { get; set; } = new();
    [JsonPropertyName("snapshot")] public string? Snapshot { get; set; }
}

/// <summary>A file system snapshot reference.</summary>
public class SnapshotPart : Part
{
    public override string Type => "snapshot";
    [JsonPropertyName("snapshot")] public string Snapshot { get; set; } = "";
}

/// <summary>A patch with file changes.</summary>
public class PatchPart : Part
{
    public override string Type => "patch";
    [JsonPropertyName("hash")] public string Hash { get; set; } = "";
    [JsonPropertyName("files")] public List<string> Files { get; set; } = [];
}

/// <summary>An agent delegation part.</summary>
public class AgentPart : Part
{
    public override string Type => "agent";
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("source")] public FilePartSourceText? Source { get; set; }
}

/// <summary>A retry attempt after an error.</summary>
public class RetryPart : Part
{
    public override string Type => "retry";
    [JsonPropertyName("attempt")] public double Attempt { get; set; }
    [JsonPropertyName("error")] public ApiErrorData Error { get; set; } = new();
    [JsonPropertyName("time")] public RetryPartTime Time { get; set; } = new();
}

/// <summary>Timestamp for a retry attempt.</summary>
public class RetryPartTime
{
    [JsonPropertyName("created")] public double Created { get; set; }
}

/// <summary>A session compaction marker.</summary>
public class CompactionPart : Part
{
    public override string Type => "compaction";
    [JsonPropertyName("auto")] public bool Auto { get; set; }
    [JsonPropertyName("overflow")] public bool? Overflow { get; set; }
}

/// <summary>Time range with start and optional end.</summary>
public class PartTimeRange
{
    [JsonPropertyName("start")] public double Start { get; set; }
    [JsonPropertyName("end")] public double? End { get; set; }
}

/// <summary>Base class for tool execution state variants.</summary>
public abstract class ToolState
{
    [JsonPropertyName("status")] public abstract string Status { get; }
    [JsonPropertyName("input")] public JsonElement? Input { get; set; }
}

/// <summary>Tool execution is pending.</summary>
public class ToolStatePending : ToolState
{
    public override string Status => "pending";
    [JsonPropertyName("raw")] public string Raw { get; set; } = "";
}

/// <summary>Tool is currently executing.</summary>
public class ToolStateRunning : ToolState
{
    public override string Status => "running";
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("time")] public PartTimeRange Time { get; set; } = new();
    [JsonPropertyName("metadata")] public Dictionary<string, JsonElement>? Metadata { get; set; }
}

/// <summary>Tool execution completed successfully.</summary>
public class ToolStateCompleted : ToolState
{
    public override string Status => "completed";
    [JsonPropertyName("output")] public string Output { get; set; } = "";
    [JsonPropertyName("title")] public string Title { get; set; } = "";
    [JsonPropertyName("metadata")] public Dictionary<string, JsonElement> Metadata { get; set; } = new();
    [JsonPropertyName("time")] public PartTimeRange Time { get; set; } = new();
    [JsonPropertyName("attachments")] public List<FilePart>? Attachments { get; set; }
}

/// <summary>Tool execution failed with an error.</summary>
public class ToolStateError : ToolState
{
    public override string Status => "error";
    [JsonPropertyName("error")] public string Error { get; set; } = "";
    [JsonPropertyName("time")] public PartTimeRange Time { get; set; } = new();
}

/// <summary>Base class for message part input variants.</summary>
public abstract class PartInput
{
    [JsonPropertyName("type")] public abstract string Type { get; }
    [JsonPropertyName("id")] public string? Id { get; set; }
}

/// <summary>Text content input for a message.</summary>
public class TextPartInput : PartInput
{
    public override string Type => "text";
    [JsonPropertyName("text")] public string Text { get; set; } = "";
    [JsonPropertyName("synthetic")] public bool? Synthetic { get; set; }
    [JsonPropertyName("ignored")] public bool? Ignored { get; set; }
    [JsonPropertyName("time")] public PartTimeRange? Time { get; set; }
    [JsonPropertyName("metadata")] public Dictionary<string, JsonElement>? Metadata { get; set; }
}

/// <summary>File attachment input for a message.</summary>
public class FilePartInput : PartInput
{
    public override string Type => "file";
    [JsonPropertyName("mime")] public string Mime { get; set; } = "";
    [JsonPropertyName("url")] public string Url { get; set; } = "";
    [JsonPropertyName("filename")] public string? Filename { get; set; }
    [JsonPropertyName("source")] public FilePartSource? Source { get; set; }
}

/// <summary>Agent delegation input for a message.</summary>
public class AgentPartInput : PartInput
{
    public override string Type => "agent";
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("source")] public FilePartSourceText? Source { get; set; }
}

/// <summary>Subtask delegation input for a message.</summary>
public class SubtaskPartInput : PartInput
{
    public override string Type => "subtask";
    [JsonPropertyName("prompt")] public string Prompt { get; set; } = "";
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("agent")] public string Agent { get; set; } = "";
    [JsonPropertyName("model")] public SubtaskModel? Model { get; set; }
    [JsonPropertyName("command")] public string? Command { get; set; }
}
