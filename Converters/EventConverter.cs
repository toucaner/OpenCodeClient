using System.Text.Json;
using System.Text.Json.Serialization;
using OpenCodeClient.Models;

namespace OpenCodeClient.Converters;

public class EventConverter : JsonConverter<Event>
{
    public override Event? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = root.TryGetProperty("type", out var t) ? t.GetString() : null;

        var json = root.GetRawText();

        return type switch
        {
            "server.connected" => JsonSerializer.Deserialize<EventServerConnected>(json, options),
            "server.heartbeat" => JsonSerializer.Deserialize<EventServerHeartbeat>(json, options),
            "global.disposed" => JsonSerializer.Deserialize<EventGlobalDisposed>(json, options),
            "tui.prompt.append" => JsonSerializer.Deserialize<EventTuiPromptAppend>(json, options),
            "tui.command.execute" => JsonSerializer.Deserialize<EventTuiCommandExecute>(json, options),
            "tui.toast.show" => JsonSerializer.Deserialize<EventTuiToastShow>(json, options),
            "tui.session.select" => JsonSerializer.Deserialize<EventTuiSessionSelect>(json, options),
            "installation.updated" => JsonSerializer.Deserialize<EventInstallationUpdated>(json, options),
            "installation.update-available" => JsonSerializer.Deserialize<EventInstallationUpdateAvailable>(json, options),
            "project.updated" => JsonSerializer.Deserialize<EventProjectUpdated>(json, options),
            "workspace.ready" => JsonSerializer.Deserialize<EventWorkspaceReady>(json, options),
            "workspace.failed" => JsonSerializer.Deserialize<EventWorkspaceFailed>(json, options),
            "server.instance.disposed" => JsonSerializer.Deserialize<EventServerInstanceDisposed>(json, options),
            "file.edited" => JsonSerializer.Deserialize<EventFileEdited>(json, options),
            "worktree.ready" => JsonSerializer.Deserialize<EventWorktreeReady>(json, options),
            "worktree.failed" => JsonSerializer.Deserialize<EventWorktreeFailed>(json, options),
            "file.watcher.updated" => JsonSerializer.Deserialize<EventFileWatcherUpdated>(json, options),
            "vcs.branch.updated" => JsonSerializer.Deserialize<EventVcsBranchUpdated>(json, options),
            "permission.asked" => JsonSerializer.Deserialize<EventPermissionAsked>(json, options),
            "permission.replied" => JsonSerializer.Deserialize<EventPermissionReplied>(json, options),
            "question.asked" => JsonSerializer.Deserialize<EventQuestionAsked>(json, options),
            "question.replied" => JsonSerializer.Deserialize<EventQuestionReplied>(json, options),
            "question.rejected" => JsonSerializer.Deserialize<EventQuestionRejected>(json, options),
            "lsp.client.diagnostics" => JsonSerializer.Deserialize<EventLspClientDiagnostics>(json, options),
            "lsp.updated" => JsonSerializer.Deserialize<EventLspUpdated>(json, options),
            "session.status" => JsonSerializer.Deserialize<EventSessionStatus>(json, options),
            "session.idle" => JsonSerializer.Deserialize<EventSessionIdle>(json, options),
            "todo.updated" => JsonSerializer.Deserialize<EventTodoUpdated>(json, options),
            "pty.created" => JsonSerializer.Deserialize<EventPtyCreated>(json, options),
            "pty.updated" => JsonSerializer.Deserialize<EventPtyUpdated>(json, options),
            "pty.exited" => JsonSerializer.Deserialize<EventPtyExited>(json, options),
            "pty.deleted" => JsonSerializer.Deserialize<EventPtyDeleted>(json, options),
            "mcp.tools.changed" => JsonSerializer.Deserialize<EventMcpToolsChanged>(json, options),
            "mcp.browser.open.failed" => JsonSerializer.Deserialize<EventMcpBrowserOpenFailed>(json, options),
            "message.updated" => JsonSerializer.Deserialize<EventMessageUpdated>(json, options),
            "message.removed" => JsonSerializer.Deserialize<EventMessageRemoved>(json, options),
            "message.part.updated" => JsonSerializer.Deserialize<EventMessagePartUpdated>(json, options),
            "message.part.delta" => JsonSerializer.Deserialize<EventMessagePartDelta>(json, options),
            "message.part.removed" => JsonSerializer.Deserialize<EventMessagePartRemoved>(json, options),
            "command.executed" => JsonSerializer.Deserialize<EventCommandExecuted>(json, options),
            "session.compacted" => JsonSerializer.Deserialize<EventSessionCompacted>(json, options),
            "session.created" => JsonSerializer.Deserialize<EventSessionCreated>(json, options),
            "session.updated" => JsonSerializer.Deserialize<EventSessionUpdated>(json, options),
            "session.deleted" => JsonSerializer.Deserialize<EventSessionDeleted>(json, options),
            "session.diff" => JsonSerializer.Deserialize<EventSessionDiff>(json, options),
            "session.error" => JsonSerializer.Deserialize<EventSessionError>(json, options),
            _ => JsonSerializer.Deserialize<EventServerConnected>(json, options)
        };
    }

    public override void Write(Utf8JsonWriter writer, Event value, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(value, value.GetType(), options);
        using var doc = JsonDocument.Parse(json);
        doc.RootElement.WriteTo(writer);
    }
}