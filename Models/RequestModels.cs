using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>
/// Request to create a new session.
/// </summary>
public class SessionCreateRequest
{
    [JsonPropertyName("parentID")] public string? ParentId { get; set; }
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("permission")] public List<PermissionRule>? Permission { get; set; }
    [JsonPropertyName("workspaceID")] public string? WorkspaceId { get; set; }
}

/// <summary>
/// Request to update session properties.
/// </summary>
public class SessionUpdateRequest
{
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("time")] public SessionUpdateTimeRequest? Time { get; set; }
}

/// <summary>
/// Time properties for session update.
/// </summary>
public class SessionUpdateTimeRequest
{
    [JsonPropertyName("archived")] public double? Archived { get; set; }
}

/// <summary>
/// Request to initialize a session with AGENTS.md.
/// </summary>
public class SessionInitRequest
{
    [JsonPropertyName("modelID")] public string ModelId { get; set; } = "";
    [JsonPropertyName("providerID")] public string ProviderId { get; set; } = "";
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
}

/// <summary>
/// Request to fork a session.
/// </summary>
public class SessionForkRequest
{
    [JsonPropertyName("messageID")] public string? MessageId { get; set; }
}

/// <summary>
/// Request to summarize a session.
/// </summary>
public class SessionSummarizeRequest
{
    [JsonPropertyName("providerID")] public string ProviderId { get; set; } = "";
    [JsonPropertyName("modelID")] public string ModelId { get; set; } = "";
    [JsonPropertyName("auto")] public bool? Auto { get; set; }
}

/// <summary>
/// Request to send a message to a session.
/// </summary>
public class SessionPromptRequest
{
    [JsonPropertyName("parts")] public List<PartInput> Parts { get; set; } = [];
    [JsonPropertyName("messageID")] public string? MessageId { get; set; }
    [JsonPropertyName("model")] public MessageModel? Model { get; set; }
    [JsonPropertyName("agent")] public string? Agent { get; set; }
    [JsonPropertyName("noReply")] public bool? NoReply { get; set; }
    [JsonPropertyName("format")] public OutputFormat? Format { get; set; }
    [JsonPropertyName("system")] public string? System { get; set; }
    [JsonPropertyName("variant")] public string? Variant { get; set; }
}

/// <summary>
/// Request to execute a slash command.
/// </summary>
public class SessionCommandRequest
{
    [JsonPropertyName("arguments")] public string Arguments { get; set; } = "";
    [JsonPropertyName("command")] public string Command { get; set; } = "";
    [JsonPropertyName("messageID")] public string? MessageId { get; set; }
    [JsonPropertyName("agent")] public string? Agent { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("variant")] public string? Variant { get; set; }
}

/// <summary>
/// Request to run a shell command.
/// </summary>
public class SessionShellRequest
{
    [JsonPropertyName("agent")] public string Agent { get; set; } = "";
    [JsonPropertyName("command")] public string Command { get; set; } = "";
    [JsonPropertyName("model")] public MessageModel? Model { get; set; }
}

/// <summary>
/// Request to revert a message.
/// </summary>
public class SessionRevertRequest
{
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
    [JsonPropertyName("partID")] public string? PartId { get; set; }
}

/// <summary>
/// Request to respond to a permission (deprecated).
/// </summary>
public class PermissionRespondRequest
{
    [JsonPropertyName("response")] public PermissionReply Response { get; set; }
}

/// <summary>
/// Request to reply to a permission request.
/// </summary>
public class PermissionReplyRequest
{
    [JsonPropertyName("reply")] public PermissionReply Reply { get; set; }
    [JsonPropertyName("message")] public string? Message { get; set; }
}

/// <summary>
/// Request to reply to a question.
/// </summary>
public class QuestionReplyRequest
{
    [JsonPropertyName("answers")] public List<List<string>> Answers { get; set; } = [];
}

/// <summary>
/// Request to create a PTY session.
/// </summary>
public class PtyCreateRequest
{
    [JsonPropertyName("command")] public string? Command { get; set; }
    [JsonPropertyName("args")] public List<string>? Args { get; set; }
    [JsonPropertyName("cwd")] public string? Cwd { get; set; }
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("env")] public Dictionary<string, string>? Env { get; set; }
}

/// <summary>
/// Request to update a PTY session.
/// </summary>
public class PtyUpdateRequest
{
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("size")] public PtySize? Size { get; set; }
}

/// <summary>
/// Terminal size in rows and columns.
/// </summary>
public class PtySize
{
    [JsonPropertyName("rows")] public double Rows { get; set; }
    [JsonPropertyName("cols")] public double Cols { get; set; }
}

/// <summary>
/// Request to update project properties.
/// </summary>
public class ProjectUpdateRequest
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("icon")] public ProjectIcon? Icon { get; set; }
    [JsonPropertyName("commands")] public ProjectCommands? Commands { get; set; }
}

/// <summary>
/// Request to initiate OAuth authorization.
/// </summary>
public class ProviderOAuthAuthorizeRequest
{
    [JsonPropertyName("method")] public int Method { get; set; }
}

/// <summary>
/// Request to complete OAuth callback.
/// </summary>
public class ProviderOAuthCallbackRequest
{
    [JsonPropertyName("method")] public int Method { get; set; }
    [JsonPropertyName("code")] public string? Code { get; set; }
}

/// <summary>
/// Request to add an MCP server.
/// </summary>
public class McpAddRequest
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("config")] public JsonElement Config { get; set; }
}

/// <summary>
/// Request to complete MCP OAuth callback.
/// </summary>
public class McpAuthCallbackRequest
{
    [JsonPropertyName("code")] public string Code { get; set; } = "";
}

/// <summary>
/// Request to create a workspace.
/// </summary>
public class WorkspaceCreateRequest
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; } = "";
    [JsonPropertyName("branch")] public string? Branch { get; set; }
    [JsonPropertyName("extra")] public JsonElement? Extra { get; set; }
}

/// <summary>
/// Request to append text to TUI prompt.
/// </summary>
public class TuiAppendPromptRequest
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
}

/// <summary>
/// Request to execute a TUI command.
/// </summary>
public class TuiExecuteCommandRequest
{
    [JsonPropertyName("command")] public string Command { get; set; } = "";
}

/// <summary>
/// Request to show a toast notification.
/// </summary>
public class TuiShowToastRequest
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
    [JsonPropertyName("variant")] public ToastVariant Variant { get; set; }
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("duration")] public double? Duration { get; set; }
}

/// <summary>
/// Request to select a session in TUI.
/// </summary>
public class TuiSelectSessionRequest
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
}

/// <summary>
/// Request to write a log entry.
/// </summary>
public class AppLogRequest
{
    [JsonPropertyName("service")] public string Service { get; set; } = "";
    [JsonPropertyName("level")] public AppLogLevel Level { get; set; }
    [JsonPropertyName("message")] public string Message { get; set; } = "";
    [JsonPropertyName("extra")] public Dictionary<string, JsonElement>? Extra { get; set; }
}

/// <summary>
/// A text search match result.
/// </summary>
public class FindTextMatch
{
    [JsonPropertyName("path")] public FindTextPath Path { get; set; } = new();
    [JsonPropertyName("lines")] public FindTextLines Lines { get; set; } = new();
    [JsonPropertyName("line_number")] public int LineNumber { get; set; }
    [JsonPropertyName("absolute_offset")] public int AbsoluteOffset { get; set; }
    [JsonPropertyName("submatches")] public List<FindTextSubmatch> Submatches { get; set; } = [];
}

/// <summary>
/// Path component of a search match.
/// </summary>
public class FindTextPath
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
}

/// <summary>
/// Line content of a search match.
/// </summary>
public class FindTextLines
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
}

/// <summary>
/// A submatch within a search result.
/// </summary>
public class FindTextSubmatch
{
    [JsonPropertyName("match")] public FindTextSubmatchText Match { get; set; } = new();
    [JsonPropertyName("start")] public int Start { get; set; }
    [JsonPropertyName("end")] public int End { get; set; }
}

/// <summary>
/// Text content of a submatch.
/// </summary>
public class FindTextSubmatchText
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
}

/// <summary>
/// Response for the next TUI control request.
/// </summary>
public class TuiControlNextResponse
{
    [JsonPropertyName("path")] public string Path { get; set; } = "";
    [JsonPropertyName("body")] public JsonElement? Body { get; set; }
}

/// <summary>
/// Response with OAuth authorization URL.
/// </summary>
public class McpAuthStartResponse
{
    [JsonPropertyName("authorizationUrl")] public string AuthorizationUrl { get; set; } = "";
}

/// <summary>
/// Response for MCP auth removal.
/// </summary>
public class McpAuthRemoveResponse
{
    [JsonPropertyName("success")] public bool Success { get; set; }
}
