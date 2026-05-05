using System.Text.Json;
using OpenCodeClient.Abstractions;
using OpenCodeClient.Events;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class SessionService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), ISessionService
{
    public Task<List<Session>> ListAsync(string? directory = null, string? workspace = null, bool? roots = null, double? start = null, string? search = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["roots"] = roots?.ToString();
        query["start"] = start?.ToString();
        query["search"] = search;
        query["limit"] = limit?.ToString();
        var url = BuildQuery("/session", query);
        return GetAsync<List<Session>>(url, cancellationToken);
    }

    public Task<Session> CreateAsync(SessionCreateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery("/session", DirWs(directory, workspace));
        return PostAsync<Session>(url, request, cancellationToken);
    }

    public Task<Dictionary<string, SessionStatus>> GetStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery("/session/status", DirWs(directory, workspace));
        return GetAsync<Dictionary<string, SessionStatus>>(url, cancellationToken);
    }

    public Task<Session> GetAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}", DirWs(directory, workspace));
        return GetAsync<Session>(url, cancellationToken);
    }

    public Task<bool> DeleteAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}", DirWs(directory, workspace));
        return DeleteAsync<bool>(url, cancellationToken);
    }

    public Task<Session> UpdateAsync(string sessionId, SessionUpdateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}", DirWs(directory, workspace));
        return PatchAsync<Session>(url, request, cancellationToken);
    }

    public Task<List<Session>> GetChildrenAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/children", DirWs(directory, workspace));
        return GetAsync<List<Session>>(url, cancellationToken);
    }

    public Task<List<Todo>> GetTodosAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/todo", DirWs(directory, workspace));
        return GetAsync<List<Todo>>(url, cancellationToken);
    }

    public Task<bool> InitAsync(string sessionId, SessionInitRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/init", DirWs(directory, workspace));
        return PostAsync<bool>(url, request, cancellationToken);
    }

    public Task<Session> ForkAsync(string sessionId, SessionForkRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/fork", DirWs(directory, workspace));
        return PostAsync<Session>(url, request, cancellationToken);
    }

    public Task<bool> AbortAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/abort", DirWs(directory, workspace));
        return PostAsync<bool>(url, cancellationToken);
    }

    public Task<Session> ShareAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/share", DirWs(directory, workspace));
        return PostAsync<Session>(url, cancellationToken);
    }

    public Task<Session> UnshareAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/share", DirWs(directory, workspace));
        return DeleteAsync<Session>(url, cancellationToken);
    }

    public Task<List<FileDiff>> GetDiffAsync(string sessionId, string? messageId = null, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["messageID"] = messageId;
        var url = BuildQuery($"/session/{sessionId}/diff", query);
        return GetAsync<List<FileDiff>>(url, cancellationToken);
    }

    public Task<bool> SummarizeAsync(string sessionId, SessionSummarizeRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/summarize", DirWs(directory, workspace));
        return PostAsync<bool>(url, request, cancellationToken);
    }

    public Task<List<MessageWithParts>> GetMessagesAsync(string sessionId, int? limit = null, string? before = null, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["limit"] = limit?.ToString();
        query["before"] = before;
        var url = BuildQuery($"/session/{sessionId}/message", query);
        return GetAsync<List<MessageWithParts>>(url, cancellationToken);
    }

    public Task<AssistantMessageWithParts> PromptAsync(string sessionId, SessionPromptRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/message", DirWs(directory, workspace));
        return PostAsync<AssistantMessageWithParts>(url, request, cancellationToken);
    }

    public Task PromptAsyncFire(string sessionId, SessionPromptRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/prompt_async", DirWs(directory, workspace));
        return PostNoContentAsync(url, request, cancellationToken);
    }

    public Task<MessageWithParts> GetMessageAsync(string sessionId, string messageId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/message/{messageId}", DirWs(directory, workspace));
        return GetAsync<MessageWithParts>(url, cancellationToken);
    }

    public Task<bool> DeleteMessageAsync(string sessionId, string messageId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/message/{messageId}", DirWs(directory, workspace));
        return DeleteAsync<bool>(url, cancellationToken);
    }

    public Task<bool> DeletePartAsync(string sessionId, string messageId, string partId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/message/{messageId}/part/{partId}", DirWs(directory, workspace));
        return DeleteAsync<bool>(url, cancellationToken);
    }

    public Task<Part> UpdatePartAsync(string sessionId, string messageId, string partId, Part part, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/message/{messageId}/part/{partId}", DirWs(directory, workspace));
        return PatchAsync<Part>(url, part, cancellationToken);
    }

    public Task<AssistantMessageWithParts> SendCommandAsync(string sessionId, SessionCommandRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/command", DirWs(directory, workspace));
        return PostAsync<AssistantMessageWithParts>(url, request, cancellationToken);
    }

    public Task<AssistantMessage> RunShellAsync(string sessionId, SessionShellRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/shell", DirWs(directory, workspace));
        return PostAsync<AssistantMessage>(url, request, cancellationToken);
    }

    public Task<Session> RevertAsync(string sessionId, SessionRevertRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/revert", DirWs(directory, workspace));
        return PostAsync<Session>(url, request, cancellationToken);
    }

    public Task<Session> UnrevertAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/unrevert", DirWs(directory, workspace));
        return PostAsync<Session>(url, cancellationToken);
    }

#pragma warning disable CS0612
    public Task<bool> RespondToPermissionAsync(string sessionId, string permissionId, PermissionRespondRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/session/{sessionId}/permissions/{permissionId}", DirWs(directory, workspace));
        return PostAsync<bool>(url, request, cancellationToken);
    }
#pragma warning restore CS0612

    public async Task<SseSubscription<Event>> SubscribeAsync(
        string sessionId,
        SseOptions options,
        string? directory = null,
        string? workspace = null,
        CancellationToken cancellationToken = default)
    {
        var subscriber = new SseSubscriber(Http, BaseUrl, directory, JsonOptions);
        return await subscriber.SubscribeAsync(options, sessionId, cancellationToken);
    }
}
