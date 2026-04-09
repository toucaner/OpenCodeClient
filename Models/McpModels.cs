using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>Base class for MCP server status variants.</summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "status")]
[JsonDerivedType(typeof(McpStatusConnected), "connected")]
[JsonDerivedType(typeof(McpStatusDisabled), "disabled")]
[JsonDerivedType(typeof(McpStatusFailed), "failed")]
[JsonDerivedType(typeof(McpStatusNeedsAuth), "needs_auth")]
[JsonDerivedType(typeof(McpStatusNeedsClientRegistration), "needs_client_registration")]
public abstract class McpStatus
{
    [JsonPropertyName("status")] public abstract string Status { get; }
}

/// <summary>MCP server is connected.</summary>
public class McpStatusConnected : McpStatus
{
    public override string Status => "connected";
}

/// <summary>MCP server is disabled.</summary>
public class McpStatusDisabled : McpStatus
{
    public override string Status => "disabled";
}

/// <summary>MCP server connection failed.</summary>
public class McpStatusFailed : McpStatus
{
    public override string Status => "failed";
    [JsonPropertyName("error")] public string Error { get; set; } = "";
}

/// <summary>MCP server requires authentication.</summary>
public class McpStatusNeedsAuth : McpStatus
{
    public override string Status => "needs_auth";
}

/// <summary>MCP server requires client registration.</summary>
public class McpStatusNeedsClientRegistration : McpStatus
{
    public override string Status => "needs_client_registration";
    [JsonPropertyName("error")] public string Error { get; set; } = "";
}

/// <summary>A resource available from an MCP server.</summary>
public class McpResource
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("uri")] public string Uri { get; set; } = "";
    [JsonPropertyName("client")] public string Client { get; set; } = "";
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("mimeType")] public string? MimeType { get; set; }
}

/// <summary>Configuration for a local MCP server.</summary>
public class McpLocalConfig
{
    [JsonPropertyName("type")] public string Type { get; set; } = "local";
    [JsonPropertyName("command")] public List<string> Command { get; set; } = [];
    [JsonPropertyName("environment")] public Dictionary<string, string>? Environment { get; set; }
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }
    [JsonPropertyName("timeout")] public int? Timeout { get; set; }
}

/// <summary>Configuration for a remote MCP server.</summary>
public class McpRemoteConfig
{
    [JsonPropertyName("type")] public string Type { get; set; } = "remote";
    [JsonPropertyName("url")] public string Url { get; set; } = "";
    [JsonPropertyName("enabled")] public bool? Enabled { get; set; }
    [JsonPropertyName("headers")] public Dictionary<string, string>? Headers { get; set; }
    [JsonPropertyName("timeout")] public int? Timeout { get; set; }
}

/// <summary>An OpenCode workspace.</summary>
public class Workspace
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("type")] public string Type { get; set; } = "";
    [JsonPropertyName("branch")] public string? Branch { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("directory")] public string? Directory { get; set; }
    [JsonPropertyName("extra")] public JsonElement? Extra { get; set; }
    [JsonPropertyName("projectID")] public string ProjectId { get; set; } = "";
}

/// <summary>A git worktree.</summary>
public class Worktree
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("branch")] public string Branch { get; set; } = "";
    [JsonPropertyName("directory")] public string Directory { get; set; } = "";
}

/// <summary>Input for creating a new worktree.</summary>
public class WorktreeCreateInput
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("startCommand")] public string? StartCommand { get; set; }
}

/// <summary>Input for removing a worktree.</summary>
public class WorktreeRemoveInput
{
    [JsonPropertyName("directory")] public string Directory { get; set; } = "";
}

/// <summary>Input for resetting a worktree.</summary>
public class WorktreeResetInput
{
    [JsonPropertyName("directory")] public string Directory { get; set; } = "";
}

/// <summary>A tool with its description and parameter schema.</summary>
public class ToolListItem
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("parameters")] public JsonElement? Parameters { get; set; }
}
