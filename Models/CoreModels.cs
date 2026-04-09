using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>Server health check response.</summary>
public record HealthResponse(
    [property: JsonPropertyName("healthy")] bool Healthy,
    [property: JsonPropertyName("version")] string Version);

/// <summary>An OpenCode project.</summary>
public class Project
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("worktree")] public string Worktree { get; set; } = "";
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("vcs")] public string? Vcs { get; set; }
    [JsonPropertyName("icon")] public ProjectIcon? Icon { get; set; }
    [JsonPropertyName("commands")] public ProjectCommands? Commands { get; set; }
    [JsonPropertyName("time")] public ProjectTime Time { get; set; } = new();
    [JsonPropertyName("sandboxes")] public List<string> Sandboxes { get; set; } = [];
}

/// <summary>Project icon configuration.</summary>
public class ProjectIcon
{
    [JsonPropertyName("url")] public string? Url { get; set; }
    [JsonPropertyName("override")] public string? Override { get; set; }
    [JsonPropertyName("color")] public string? Color { get; set; }
}

/// <summary>Project startup commands.</summary>
public class ProjectCommands
{
    [JsonPropertyName("start")] public string? Start { get; set; }
}

/// <summary>Project timestamps.</summary>
public class ProjectTime
{
    [JsonPropertyName("created")] public double Created { get; set; }
    [JsonPropertyName("updated")] public double Updated { get; set; }
    [JsonPropertyName("initialized")] public double? Initialized { get; set; }
}

/// <summary>Abbreviated project information.</summary>
public class ProjectSummary
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("worktree")] public string Worktree { get; set; } = "";
    [JsonPropertyName("name")] public string? Name { get; set; }
}

/// <summary>A pseudo-terminal (PTY) session.</summary>
public class Pty
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("title")] public string Title { get; set; } = "";
    [JsonPropertyName("command")] public string Command { get; set; } = "";
    [JsonPropertyName("args")] public List<string> Args { get; set; } = [];
    [JsonPropertyName("cwd")] public string Cwd { get; set; } = "";
    [JsonPropertyName("status")] public PtyStatus Status { get; set; }
    [JsonPropertyName("pid")] public double Pid { get; set; }
}

/// <summary>A file diff showing changes.</summary>
public class FileDiff
{
    [JsonPropertyName("file")] public string File { get; set; } = "";
    [JsonPropertyName("before")] public string Before { get; set; } = "";
    [JsonPropertyName("after")] public string After { get; set; } = "";
    [JsonPropertyName("additions")] public double Additions { get; set; }
    [JsonPropertyName("deletions")] public double Deletions { get; set; }
    [JsonPropertyName("status")] public FileDiffStatus? Status { get; set; }
}

/// <summary>A session todo item.</summary>
public class Todo
{
    [JsonPropertyName("content")] public string Content { get; set; } = "";
    [JsonPropertyName("status")] public string Status { get; set; } = "";
    [JsonPropertyName("priority")] public string Priority { get; set; } = "";
}

/// <summary>A text range with start and end positions.</summary>
public class Range
{
    [JsonPropertyName("start")] public Position Start { get; set; } = new();
    [JsonPropertyName("end")] public Position End { get; set; } = new();
}

/// <summary>A position in a text document.</summary>
public class Position
{
    [JsonPropertyName("line")] public double Line { get; set; }
    [JsonPropertyName("character")] public double Character { get; set; }
}

/// <summary>A workspace symbol (function, class, variable, etc.).</summary>
public class Symbol
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("kind")] public int Kind { get; set; }
    [JsonPropertyName("location")] public SymbolLocation Location { get; set; } = new();
}

/// <summary>Location of a symbol in a file.</summary>
public class SymbolLocation
{
    [JsonPropertyName("uri")] public string Uri { get; set; } = "";
    [JsonPropertyName("range")] public Range Range { get; set; } = new();
}

/// <summary>A file or directory node in the file tree.</summary>
public class FileNode
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("path")] public string Path { get; set; } = "";
    [JsonPropertyName("absolute")] public string Absolute { get; set; } = "";
    [JsonPropertyName("type")] public FileNodeType Type { get; set; }
    [JsonPropertyName("ignored")] public bool Ignored { get; set; }
}

/// <summary>Content of a file.</summary>
public class FileContent
{
    [JsonPropertyName("type")] public FileContentType Type { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; } = "";
    [JsonPropertyName("diff")] public string? Diff { get; set; }
    [JsonPropertyName("encoding")] public string? Encoding { get; set; }
    [JsonPropertyName("mimeType")] public string? MimeType { get; set; }
}

/// <summary>Git status of a tracked file.</summary>
public class FileStatus
{
    [JsonPropertyName("path")] public string Path { get; set; } = "";
    [JsonPropertyName("added")] public int Added { get; set; }
    [JsonPropertyName("removed")] public int Removed { get; set; }
    [JsonPropertyName("status")] public FileStatusType Status { get; set; }
}

/// <summary>Path information for the OpenCode instance.</summary>
public class PathInfo
{
    [JsonPropertyName("home")] public string Home { get; set; } = "";
    [JsonPropertyName("state")] public string State { get; set; } = "";
    [JsonPropertyName("config")] public string Config { get; set; } = "";
    [JsonPropertyName("worktree")] public string Worktree { get; set; } = "";
    [JsonPropertyName("directory")] public string Directory { get; set; } = "";
}

/// <summary>Version control system information.</summary>
public record VcsInfo([property: JsonPropertyName("branch")] string Branch);

/// <summary>An OpenCode command definition.</summary>
public class Command
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("template")] public string Template { get; set; } = "";
    [JsonPropertyName("hints")] public string Hints { get; set; } = "";
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("agent")] public string? Agent { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("subtask")] public string? Subtask { get; set; }
    [JsonPropertyName("source")] public CommandSource? Source { get; set; }
}

/// <summary>An AI agent configuration.</summary>
public class Agent
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("mode")] public AgentMode Mode { get; set; }
    [JsonPropertyName("permission")] public List<PermissionRule>? Permission { get; set; }
    [JsonPropertyName("options")] public JsonElement? Options { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("native")] public bool? Native { get; set; }
    [JsonPropertyName("hidden")] public bool? Hidden { get; set; }
    [JsonPropertyName("topP")] public double? TopP { get; set; }
    [JsonPropertyName("temperature")] public double? Temperature { get; set; }
    [JsonPropertyName("color")] public string? Color { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("variant")] public string? Variant { get; set; }
    [JsonPropertyName("prompt")] public string? Prompt { get; set; }
    [JsonPropertyName("steps")] public int? Steps { get; set; }
}

/// <summary>A permission rule with action.</summary>
public class PermissionRule
{
    [JsonPropertyName("permission")] public string Permission { get; set; } = "";
    [JsonPropertyName("pattern")] public string Pattern { get; set; } = "";
    [JsonPropertyName("action")] public PermissionActionValue Action { get; set; }
}

/// <summary>A pending permission request from the AI.</summary>
public class PermissionRequest
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("permission")] public string Permission { get; set; } = "";
    [JsonPropertyName("patterns")] public List<string> Patterns { get; set; } = [];
    [JsonPropertyName("metadata")] public Dictionary<string, JsonElement>? Metadata { get; set; }
    [JsonPropertyName("always")] public List<string> Always { get; set; } = [];
    [JsonPropertyName("tool")] public PermissionTool? Tool { get; set; }
}

/// <summary>Tool information associated with a permission request.</summary>
public class PermissionTool
{
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
    [JsonPropertyName("callID")] public string CallId { get; set; } = "";
}

/// <summary>An option in a question.</summary>
public class QuestionOption
{
    [JsonPropertyName("label")] public string Label { get; set; } = "";
    [JsonPropertyName("description")] public string Description { get; set; } = "";
}

/// <summary>A question with options.</summary>
public class QuestionInfo
{
    [JsonPropertyName("question")] public string Question { get; set; } = "";
    [JsonPropertyName("header")] public string Header { get; set; } = "";
    [JsonPropertyName("options")] public List<QuestionOption> Options { get; set; } = [];
    [JsonPropertyName("multiple")] public bool? Multiple { get; set; }
    [JsonPropertyName("custom")] public bool? Custom { get; set; }
}

/// <summary>A pending question request from the AI.</summary>
public class QuestionRequest
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("questions")] public List<QuestionInfo> Questions { get; set; } = [];
    [JsonPropertyName("tool")] public PermissionTool? Tool { get; set; }
}

/// <summary>An agent skill definition.</summary>
public class Skill
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("location")] public string Location { get; set; } = "";
    [JsonPropertyName("content")] public string Content { get; set; } = "";
}

/// <summary>Status of an LSP server.</summary>
public class LspStatus
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("root")] public string Root { get; set; } = "";
    [JsonPropertyName("status")] public string Status { get; set; } = "";
}

/// <summary>Status of a code formatter.</summary>
public class FormatterStatus
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("extensions")] public List<string> Extensions { get; set; } = [];
    [JsonPropertyName("enabled")] public bool Enabled { get; set; }
}
