using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>
/// A global event wrapper with directory context.
/// </summary>
public class GlobalEvent
{
    [JsonPropertyName("directory")] public string Directory { get; set; } = "";
    [JsonPropertyName("payload")] public Event Payload { get; set; } = null!;
}

/// <summary>
/// Base class for all server-sent event types.
/// </summary>
public abstract class Event
{
    [JsonPropertyName("type")] public abstract string Type { get; }

    public string? GetSessionId() => this switch
    {
        EventSessionCreated e => e.Properties.Info.Id,
        EventSessionUpdated e => e.Properties.Info.Id,
        EventSessionDeleted e => e.Properties.Info.Id,
        EventSessionIdle e => e.Properties.SessionId,
        EventSessionError e => e.Properties.SessionId,
        EventMessageUpdated e => e.Properties.Info.SessionId,
        EventMessageRemoved e => e.Properties.SessionId,
        EventMessagePartUpdated e => e.Properties.Part?.SessionId,
        EventMessagePartDelta e => e.Properties.SessionId,
        EventPermissionAsked e => e.Properties.SessionId,
        EventPermissionReplied e => e.Properties.SessionId,
        _ => null
    };

    public bool IsHeartbeat() => this is EventServerHeartbeat;

    public bool IsConnected() => this is EventServerConnected;
}

/// <summary>
/// Event: server connected.
/// </summary>
public class EventServerConnected : Event
{
    public override string Type => "server.connected";
    [JsonPropertyName("properties")] public EmptyProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: server heartbeat.
/// </summary>
public class EventServerHeartbeat : Event
{
    public override string Type => "server.heartbeat";
    [JsonPropertyName("properties")] public EmptyProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: global instance disposed.
/// </summary>
public class EventGlobalDisposed : Event
{
    public override string Type => "global.disposed";
    [JsonPropertyName("properties")] public EmptyProperties Properties { get; set; } = new();
}

/// <summary>
/// Empty event properties payload.
/// </summary>
public class EmptyProperties { }

/// <summary>
/// Event: text appended to TUI prompt.
/// </summary>
public class EventTuiPromptAppend : Event
{
    public override string Type => "tui.prompt.append";
    [JsonPropertyName("properties")] public TuiPromptAppendProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for TUI prompt append event.
/// </summary>
public class TuiPromptAppendProperties
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
}

/// <summary>
/// Event: TUI command executed.
/// </summary>
public class EventTuiCommandExecute : Event
{
    public override string Type => "tui.command.execute";
    [JsonPropertyName("properties")] public TuiCommandExecuteProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for TUI command execute event.
/// </summary>
public class TuiCommandExecuteProperties
{
    [JsonPropertyName("command")] public string Command { get; set; } = "";
}

/// <summary>
/// Event: toast notification shown in TUI.
/// </summary>
public class EventTuiToastShow : Event
{
    public override string Type => "tui.toast.show";
    [JsonPropertyName("properties")] public TuiToastShowProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for TUI toast show event.
/// </summary>
public class TuiToastShowProperties
{
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("message")] public string Message { get; set; } = "";
    [JsonPropertyName("variant")] public ToastVariant Variant { get; set; }
    [JsonPropertyName("duration")] public double? Duration { get; set; }
}

/// <summary>
/// Event: session selected in TUI.
/// </summary>
public class EventTuiSessionSelect : Event
{
    public override string Type => "tui.session.select";
    [JsonPropertyName("properties")] public TuiSessionSelectProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for TUI session select event.
/// </summary>
public class TuiSessionSelectProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
}

/// <summary>
/// Event: installation updated.
/// </summary>
public class EventInstallationUpdated : Event
{
    public override string Type => "installation.updated";
    [JsonPropertyName("properties")] public InstallationVersionProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: installation update available.
/// </summary>
public class EventInstallationUpdateAvailable : Event
{
    public override string Type => "installation.update-available";
    [JsonPropertyName("properties")] public InstallationVersionProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing a version string.
/// </summary>
public class InstallationVersionProperties
{
    [JsonPropertyName("version")] public string Version { get; set; } = "";
}

/// <summary>
/// Event: project updated.
/// </summary>
public class EventProjectUpdated : Event
{
    public override string Type => "project.updated";
    [JsonPropertyName("properties")] public Project Properties { get; set; } = new();
}

/// <summary>
/// Event: workspace is ready.
/// </summary>
public class EventWorkspaceReady : Event
{
    public override string Type => "workspace.ready";
    [JsonPropertyName("properties")] public NameProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: workspace initialization failed.
/// </summary>
public class EventWorkspaceFailed : Event
{
    public override string Type => "workspace.failed";
    [JsonPropertyName("properties")] public MessageProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing a name.
/// </summary>
public class NameProperties
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
}

/// <summary>
/// Properties containing a message.
/// </summary>
public class MessageProperties
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
}

/// <summary>
/// Event: server instance disposed.
/// </summary>
public class EventServerInstanceDisposed : Event
{
    public override string Type => "server.instance.disposed";
    [JsonPropertyName("properties")] public DirectoryProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing a directory path.
/// </summary>
public class DirectoryProperties
{
    [JsonPropertyName("directory")] public string Directory { get; set; } = "";
}

/// <summary>
/// Event: file was edited.
/// </summary>
public class EventFileEdited : Event
{
    public override string Type => "file.edited";
    [JsonPropertyName("properties")] public FileEditedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for file edited event.
/// </summary>
public class FileEditedProperties
{
    [JsonPropertyName("file")] public string File { get; set; } = "";
}

/// <summary>
/// Event: worktree is ready.
/// </summary>
public class EventWorktreeReady : Event
{
    public override string Type => "worktree.ready";
    [JsonPropertyName("properties")] public WorktreeReadyProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for worktree ready event.
/// </summary>
public class WorktreeReadyProperties
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("branch")] public string Branch { get; set; } = "";
}

/// <summary>
/// Event: worktree initialization failed.
/// </summary>
public class EventWorktreeFailed : Event
{
    public override string Type => "worktree.failed";
    [JsonPropertyName("properties")] public MessageProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: file watcher detected a change.
/// </summary>
public class EventFileWatcherUpdated : Event
{
    public override string Type => "file.watcher.updated";
    [JsonPropertyName("properties")] public FileWatcherUpdatedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for file watcher event.
/// </summary>
public class FileWatcherUpdatedProperties
{
    [JsonPropertyName("file")] public string File { get; set; } = "";
    [JsonPropertyName("event")] public string EventType { get; set; } = "";
}

/// <summary>
/// Event: VCS branch changed.
/// </summary>
public class EventVcsBranchUpdated : Event
{
    public override string Type => "vcs.branch.updated";
    [JsonPropertyName("properties")] public VcsBranchProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for VCS branch event.
/// </summary>
public class VcsBranchProperties
{
    [JsonPropertyName("branch")] public string? Branch { get; set; }
}

/// <summary>
/// Event: permission requested by AI.
/// </summary>
public class EventPermissionAsked : Event
{
    public override string Type => "permission.asked";
    [JsonPropertyName("properties")] public PermissionRequest Properties { get; set; } = new();
}

/// <summary>
/// Event: permission request answered.
/// </summary>
public class EventPermissionReplied : Event
{
    public override string Type => "permission.replied";
    [JsonPropertyName("properties")] public PermissionRepliedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for permission reply event.
/// </summary>
public class PermissionRepliedProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("requestID")] public string RequestId { get; set; } = "";
    [JsonPropertyName("reply")] public PermissionReply Reply { get; set; }
}

/// <summary>
/// Event: question asked by AI.
/// </summary>
public class EventQuestionAsked : Event
{
    public override string Type => "question.asked";
    [JsonPropertyName("properties")] public QuestionRequest Properties { get; set; } = new();
}

/// <summary>
/// Event: question answered.
/// </summary>
public class EventQuestionReplied : Event
{
    public override string Type => "question.replied";
    [JsonPropertyName("properties")] public QuestionRepliedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for question reply event.
/// </summary>
public class QuestionRepliedProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("requestID")] public string RequestId { get; set; } = "";
    [JsonPropertyName("answers")] public List<List<string>> Answers { get; set; } = [];
}

/// <summary>
/// Event: question rejected.
/// </summary>
public class EventQuestionRejected : Event
{
    public override string Type => "question.rejected";
    [JsonPropertyName("properties")] public SessionRequestProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties with session and request IDs.
/// </summary>
public class SessionRequestProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("requestID")] public string RequestId { get; set; } = "";
}

/// <summary>
/// Event: LSP diagnostics received.
/// </summary>
public class EventLspClientDiagnostics : Event
{
    public override string Type => "lsp.client.diagnostics";
    [JsonPropertyName("properties")] public LspDiagnosticsProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for LSP diagnostics event.
/// </summary>
public class LspDiagnosticsProperties
{
    [JsonPropertyName("serverID")] public string ServerId { get; set; } = "";
    [JsonPropertyName("path")] public string Path { get; set; } = "";
}

/// <summary>
/// Event: LSP status updated.
/// </summary>
public class EventLspUpdated : Event
{
    public override string Type => "lsp.updated";
    [JsonPropertyName("properties")] public EmptyProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: session status changed.
/// </summary>
public class EventSessionStatus : Event
{
    public override string Type => "session.status";
    [JsonPropertyName("properties")] public SessionStatusProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for session status event.
/// </summary>
public class SessionStatusProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("status")] public SessionStatus Status { get; set; } = null!;
}

/// <summary>
/// Event: session became idle.
/// </summary>
public class EventSessionIdle : Event
{
    public override string Type => "session.idle";
    [JsonPropertyName("properties")] public SessionIdProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing a session ID.
/// </summary>
public class SessionIdProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
}

/// <summary>
/// Event: session todos updated.
/// </summary>
public class EventTodoUpdated : Event
{
    public override string Type => "todo.updated";
    [JsonPropertyName("properties")] public TodoUpdatedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for todo updated event.
/// </summary>
public class TodoUpdatedProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("todos")] public List<Todo> Todos { get; set; } = [];
}

/// <summary>
/// Event: PTY session created.
/// </summary>
public class EventPtyCreated : Event
{
    public override string Type => "pty.created";
    [JsonPropertyName("properties")] public PtyInfoProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: PTY session updated.
/// </summary>
public class EventPtyUpdated : Event
{
    public override string Type => "pty.updated";
    [JsonPropertyName("properties")] public PtyInfoProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing PTY info.
/// </summary>
public class PtyInfoProperties
{
    [JsonPropertyName("info")] public Pty Info { get; set; } = new();
}

/// <summary>
/// Event: PTY session exited.
/// </summary>
public class EventPtyExited : Event
{
    public override string Type => "pty.exited";
    [JsonPropertyName("properties")] public PtyExitedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for PTY exited event.
/// </summary>
public class PtyExitedProperties
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("exitCode")] public double ExitCode { get; set; }
}

/// <summary>
/// Event: PTY session deleted.
/// </summary>
public class EventPtyDeleted : Event
{
    public override string Type => "pty.deleted";
    [JsonPropertyName("properties")] public PtyDeletedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for PTY deleted event.
/// </summary>
public class PtyDeletedProperties
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
}

/// <summary>
/// Event: MCP tools changed.
/// </summary>
public class EventMcpToolsChanged : Event
{
    public override string Type => "mcp.tools.changed";
    [JsonPropertyName("properties")] public McpServerProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing MCP server name.
/// </summary>
public class McpServerProperties
{
    [JsonPropertyName("server")] public string Server { get; set; } = "";
}

/// <summary>
/// Event: MCP browser open failed.
/// </summary>
public class EventMcpBrowserOpenFailed : Event
{
    public override string Type => "mcp.browser.open.failed";
    [JsonPropertyName("properties")] public McpBrowserOpenFailedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for MCP browser open failed event.
/// </summary>
public class McpBrowserOpenFailedProperties
{
    [JsonPropertyName("mcpName")] public string McpName { get; set; } = "";
    [JsonPropertyName("url")] public string Url { get; set; } = "";
}

/// <summary>
/// Event: message updated.
/// </summary>
public class EventMessageUpdated : Event
{
    public override string Type => "message.updated";
    [JsonPropertyName("properties")] public MessageInfoProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing a message.
/// </summary>
public class MessageInfoProperties
{
    [JsonPropertyName("info")] public Message Info { get; set; } = null!;
}

/// <summary>
/// Event: message removed.
/// </summary>
public class EventMessageRemoved : Event
{
    public override string Type => "message.removed";
    [JsonPropertyName("properties")] public MessageRemovedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for message removed event.
/// </summary>
public class MessageRemovedProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
}

/// <summary>
/// Event: message part updated.
/// </summary>
public class EventMessagePartUpdated : Event
{
    public override string Type => "message.part.updated";
    [JsonPropertyName("properties")] public PartProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing a message part.
/// </summary>
public class PartProperties
{
    [JsonPropertyName("part")] public Part Part { get; set; } = null!;
}

/// <summary>
/// Event: incremental message part update.
/// </summary>
public class EventMessagePartDelta : Event
{
    public override string Type => "message.part.delta";
    [JsonPropertyName("properties")] public PartDeltaProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for part delta event.
/// </summary>
public class PartDeltaProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
    [JsonPropertyName("partID")] public string PartId { get; set; } = "";
    [JsonPropertyName("field")] public string Field { get; set; } = "";
    [JsonPropertyName("delta")] public string Delta { get; set; } = "";
}

/// <summary>
/// Event: message part removed.
/// </summary>
public class EventMessagePartRemoved : Event
{
    public override string Type => "message.part.removed";
    [JsonPropertyName("properties")] public PartRemovedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for part removed event.
/// </summary>
public class PartRemovedProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
    [JsonPropertyName("partID")] public string PartId { get; set; } = "";
}

/// <summary>
/// Event: command executed.
/// </summary>
public class EventCommandExecuted : Event
{
    public override string Type => "command.executed";
    [JsonPropertyName("properties")] public CommandExecutedProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for command executed event.
/// </summary>
public class CommandExecutedProperties
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("arguments")] public string Arguments { get; set; } = "";
    [JsonPropertyName("messageID")] public string MessageId { get; set; } = "";
}

/// <summary>
/// Event: session compacted.
/// </summary>
public class EventSessionCompacted : Event
{
    public override string Type => "session.compacted";
    [JsonPropertyName("properties")] public SessionIdProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: session created.
/// </summary>
public class EventSessionCreated : Event
{
    public override string Type => "session.created";
    [JsonPropertyName("properties")] public SessionInfoProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: session updated.
/// </summary>
public class EventSessionUpdated : Event
{
    public override string Type => "session.updated";
    [JsonPropertyName("properties")] public SessionInfoProperties Properties { get; set; } = new();
}

/// <summary>
/// Event: session deleted.
/// </summary>
public class EventSessionDeleted : Event
{
    public override string Type => "session.deleted";
    [JsonPropertyName("properties")] public SessionInfoProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties containing session info.
/// </summary>
public class SessionInfoProperties
{
    [JsonPropertyName("info")] public Session Info { get; set; } = new();
}

/// <summary>
/// Event: session diff updated.
/// </summary>
public class EventSessionDiff : Event
{
    public override string Type => "session.diff";
    [JsonPropertyName("properties")] public SessionDiffProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for session diff event.
/// </summary>
public class SessionDiffProperties
{
    [JsonPropertyName("sessionID")] public string SessionId { get; set; } = "";
    [JsonPropertyName("diff")] public List<FileDiff> Diff { get; set; } = [];
}

/// <summary>
/// Event: session error occurred.
/// </summary>
public class EventSessionError : Event
{
    public override string Type => "session.error";
    [JsonPropertyName("properties")] public SessionErrorProperties Properties { get; set; } = new();
}

/// <summary>
/// Properties for session error event.
/// </summary>
public class SessionErrorProperties
{
    [JsonPropertyName("sessionID")] public string? SessionId { get; set; }
    [JsonPropertyName("error")] public MessageError? Error { get; set; }
}
