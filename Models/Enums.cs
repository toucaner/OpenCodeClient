using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>Log severity level.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<LogLevel>))]
public enum LogLevel
{
    [JsonPropertyName("DEBUG")] Debug,
    [JsonPropertyName("INFO")] Info,
    [JsonPropertyName("WARN")] Warn,
    [JsonPropertyName("ERROR")] Error
}

/// <summary>Permission action type.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<PermissionActionValue>))]
public enum PermissionActionValue
{
    [JsonPropertyName("allow")] Allow,
    [JsonPropertyName("deny")] Deny,
    [JsonPropertyName("ask")] Ask
}

/// <summary>Reply to a permission request.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<PermissionReply>))]
public enum PermissionReply
{
    [JsonPropertyName("once")] Once,
    [JsonPropertyName("always")] Always,
    [JsonPropertyName("reject")] Reject
}

/// <summary>Status of a PTY session.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<PtyStatus>))]
public enum PtyStatus
{
    [JsonPropertyName("running")] Running,
    [JsonPropertyName("exited")] Exited
}

/// <summary>Status of a file in a diff.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<FileDiffStatus>))]
public enum FileDiffStatus
{
    [JsonPropertyName("added")] Added,
    [JsonPropertyName("deleted")] Deleted,
    [JsonPropertyName("modified")] Modified
}

/// <summary>Type of a file system node.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<FileNodeType>))]
public enum FileNodeType
{
    [JsonPropertyName("file")] File,
    [JsonPropertyName("directory")] Directory
}

/// <summary>Content type of a file.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<FileContentType>))]
public enum FileContentType
{
    [JsonPropertyName("text")] Text,
    [JsonPropertyName("binary")] Binary
}

/// <summary>Git status of a file.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<FileStatusType>))]
public enum FileStatusType
{
    [JsonPropertyName("added")] Added,
    [JsonPropertyName("deleted")] Deleted,
    [JsonPropertyName("modified")] Modified
}

/// <summary>Variant of a toast notification.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<ToastVariant>))]
public enum ToastVariant
{
    [JsonPropertyName("info")] Info,
    [JsonPropertyName("success")] Success,
    [JsonPropertyName("warning")] Warning,
    [JsonPropertyName("error")] Error
}

/// <summary>Application log level.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<AppLogLevel>))]
public enum AppLogLevel
{
    [JsonPropertyName("debug")] Debug,
    [JsonPropertyName("info")] Info,
    [JsonPropertyName("warn")] Warn,
    [JsonPropertyName("error")] Error
}

/// <summary>Agent execution mode.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<AgentMode>))]
public enum AgentMode
{
    [JsonPropertyName("subagent")] Subagent,
    [JsonPropertyName("primary")] Primary,
    [JsonPropertyName("all")] All
}

/// <summary>Source of an AI provider configuration.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<ProviderSource>))]
public enum ProviderSource
{
    [JsonPropertyName("env")] Env,
    [JsonPropertyName("config")] Config,
    [JsonPropertyName("custom")] Custom,
    [JsonPropertyName("api")] Api
}

/// <summary>Source of a command definition.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<CommandSource>))]
public enum CommandSource
{
    [JsonPropertyName("command")] Command,
    [JsonPropertyName("mcp")] Mcp,
    [JsonPropertyName("skill")] Skill
}

/// <summary>Release status of a model.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<ModelStatusValue>))]
public enum ModelStatusValue
{
    [JsonPropertyName("alpha")] Alpha,
    [JsonPropertyName("beta")] Beta,
    [JsonPropertyName("deprecated")] Deprecated,
    [JsonPropertyName("active")] Active
}

/// <summary>Layout configuration mode.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<LayoutConfig>))]
public enum LayoutConfig
{
    [JsonPropertyName("auto")] Auto,
    [JsonPropertyName("stretch")] Stretch
}

/// <summary>File watcher event type.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<FileWatcherEvent>))]
public enum FileWatcherEvent
{
    [JsonPropertyName("add")] Add,
    [JsonPropertyName("change")] Change,
    [JsonPropertyName("unlink")] Unlink
}
