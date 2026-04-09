using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>An OpenCode AI session.</summary>
public class Session
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("slug")] public string Slug { get; set; } = "";
    [JsonPropertyName("projectID")] public string ProjectId { get; set; } = "";
    [JsonPropertyName("directory")] public string Directory { get; set; } = "";
    [JsonPropertyName("title")] public string Title { get; set; } = "";
    [JsonPropertyName("version")] public string Version { get; set; } = "";
    [JsonPropertyName("workspaceID")] public string? WorkspaceId { get; set; }
    [JsonPropertyName("parentID")] public string? ParentId { get; set; }
    [JsonPropertyName("summary")] public SessionSummary? Summary { get; set; }
    [JsonPropertyName("share")] public SessionShare? Share { get; set; }
    [JsonPropertyName("time")] public SessionTime Time { get; set; } = new();
    [JsonPropertyName("permission")] public List<PermissionRule>? Permission { get; set; }
    [JsonPropertyName("revert")] public SessionRevert? Revert { get; set; }
}

/// <summary>Summary of session changes.</summary>
public class SessionSummary
{
    [JsonPropertyName("additions")] public double Additions { get; set; }
    [JsonPropertyName("deletions")] public double Deletions { get; set; }
    [JsonPropertyName("files")] public double Files { get; set; }
    [JsonPropertyName("diffs")] public List<FileDiff>? Diffs { get; set; }
}

/// <summary>Session sharing information.</summary>
public class SessionShare
{
    [JsonPropertyName("url")] public string Url { get; set; } = "";
}

/// <summary>Session timestamps.</summary>
public class SessionTime
{
    [JsonPropertyName("created")] public double Created { get; set; }
    [JsonPropertyName("updated")] public double Updated { get; set; }
    [JsonPropertyName("compacting")] public double? Compacting { get; set; }
    [JsonPropertyName("archived")] public double? Archived { get; set; }
}

/// <summary>Session revert state.</summary>
public class SessionRevert
{
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
    [JsonPropertyName("partID")] public string? PartId { get; set; }
    [JsonPropertyName("snapshot")] public string? Snapshot { get; set; }
    [JsonPropertyName("diff")] public string? Diff { get; set; }
}

/// <summary>A session with cross-project context.</summary>
public class GlobalSession : Session
{
    [JsonPropertyName("project")] public ProjectSummary? Project { get; set; }
}

/// <summary>Base class for session status variants.</summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(SessionStatusIdle), "idle")]
[JsonDerivedType(typeof(SessionStatusBusy), "busy")]
[JsonDerivedType(typeof(SessionStatusRetry), "retry")]
public abstract class SessionStatus
{
    [JsonPropertyName("type")] public abstract string Type { get; }
}

/// <summary>Session is idle.</summary>
public class SessionStatusIdle : SessionStatus
{
    public override string Type => "idle";
}

/// <summary>Session is busy processing.</summary>
public class SessionStatusBusy : SessionStatus
{
    public override string Type => "busy";
}

/// <summary>Session is retrying after an error.</summary>
public class SessionStatusRetry : SessionStatus
{
    public override string Type => "retry";
    [JsonPropertyName("attempt")] public double Attempt { get; set; }
    [JsonPropertyName("message")] public string Message { get; set; } = "";
    [JsonPropertyName("next")] public double Next { get; set; }
}

/// <summary>Base class for session messages.</summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "role")]
[JsonDerivedType(typeof(UserMessage), "user")]
[JsonDerivedType(typeof(AssistantMessage), "assistant")]
public abstract class Message
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("role")] public abstract string Role { get; }
}

/// <summary>A message sent by the user.</summary>
public class UserMessage : Message
{
    public override string Role => "user";
    [JsonPropertyName("time")] public MessageCreatedTime Time { get; set; } = new();
    [JsonPropertyName("format")] public OutputFormat? Format { get; set; }
    [JsonPropertyName("agent")] public string Agent { get; set; } = "";
    [JsonPropertyName("model")] public MessageModel Model { get; set; } = new();
    [JsonPropertyName("system")] public string? System { get; set; }
    [JsonPropertyName("variant")] public string? Variant { get; set; }
    [JsonPropertyName("tools")] public Dictionary<string, bool>? Tools { get; set; }
}

/// <summary>A message from the AI assistant.</summary>
public class AssistantMessage : Message
{
    public override string Role => "assistant";
    [JsonPropertyName("time")] public AssistantMessageTime Time { get; set; } = new();
    [JsonPropertyName("error")] public MessageError? Error { get; set; }
    [JsonPropertyName("parentID")] public string ParentId { get; set; } = "";
    [JsonPropertyName("modelID")] public string ModelId { get; set; } = "";
    [JsonPropertyName("providerID")] public string ProviderId { get; set; } = "";
    [JsonPropertyName("mode")] public string Mode { get; set; } = "";
    [JsonPropertyName("agent")] public string Agent { get; set; } = "";
    [JsonPropertyName("path")] public AssistantMessagePath Path { get; set; } = new();
    [JsonPropertyName("summary")] public bool? Summary { get; set; }
    [JsonPropertyName("cost")] public double Cost { get; set; }
    [JsonPropertyName("tokens")] public TokenUsage Tokens { get; set; } = new();
    [JsonPropertyName("structured")] public JsonElement? Structured { get; set; }
    [JsonPropertyName("variant")] public string? Variant { get; set; }
    [JsonPropertyName("finish")] public string? Finish { get; set; }
}

/// <summary>Timestamp for message creation.</summary>
public class MessageCreatedTime
{
    [JsonPropertyName("created")] public double Created { get; set; }
}

/// <summary>Timestamps for assistant message lifecycle.</summary>
public class AssistantMessageTime
{
    [JsonPropertyName("created")] public double Created { get; set; }
    [JsonPropertyName("completed")] public double? Completed { get; set; }
}

/// <summary>Provider and model identifiers for a message.</summary>
public class MessageModel
{
    [JsonPropertyName("providerID")] public string ProviderId { get; set; } = "";
    [JsonPropertyName("modelID")] public string ModelId { get; set; } = "";
}

/// <summary>Working directory paths for an assistant message.</summary>
public class AssistantMessagePath
{
    [JsonPropertyName("cwd")] public string Cwd { get; set; } = "";
    [JsonPropertyName("root")] public string Root { get; set; } = "";
}

/// <summary>Token usage statistics for a message.</summary>
public class TokenUsage
{
    [JsonPropertyName("total")] public double? Total { get; set; }
    [JsonPropertyName("input")] public double Input { get; set; }
    [JsonPropertyName("output")] public double Output { get; set; }
    [JsonPropertyName("reasoning")] public double Reasoning { get; set; }
    [JsonPropertyName("cache")] public TokenCache Cache { get; set; } = new();
}

/// <summary>Cache token read/write statistics.</summary>
public class TokenCache
{
    [JsonPropertyName("read")] public double Read { get; set; }
    [JsonPropertyName("write")] public double Write { get; set; }
}

/// <summary>A message bundled with its parts.</summary>
public class MessageWithParts
{
    [JsonPropertyName("info")] public Message Info { get; set; } = null!;
    [JsonPropertyName("parts")] public List<Part> Parts { get; set; } = [];
}

/// <summary>An assistant message bundled with its parts.</summary>
public class AssistantMessageWithParts
{
    [JsonPropertyName("info")] public AssistantMessage Info { get; set; } = null!;
    [JsonPropertyName("parts")] public List<Part> Parts { get; set; } = [];
}
