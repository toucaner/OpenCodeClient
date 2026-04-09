using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class ExperimentalService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IExperimentalService
{
    public Task<List<string>> GetToolIdsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<string>>(BuildQuery("/experimental/tool/ids", DirWs(directory, workspace)), cancellationToken);

    public Task<List<ToolListItem>> ListToolsAsync(string provider, string model, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["provider"] = provider;
        query["model"] = model;
        return GetAsync<List<ToolListItem>>(BuildQuery("/experimental/tool", query), cancellationToken);
    }

    public Task<Workspace> CreateWorkspaceAsync(WorkspaceCreateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<Workspace>(BuildQuery("/experimental/workspace", DirWs(directory, workspace)), request, cancellationToken);

    public Task<List<Workspace>> ListWorkspacesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<Workspace>>(BuildQuery("/experimental/workspace", DirWs(directory, workspace)), cancellationToken);

    public Task<Workspace> RemoveWorkspaceAsync(string id, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => DeleteAsync<Workspace>(BuildQuery($"/experimental/workspace/{Uri.EscapeDataString(id)}", DirWs(directory, workspace)), cancellationToken);

    public Task<Worktree> CreateWorktreeAsync(WorktreeCreateInput input, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<Worktree>(BuildQuery("/experimental/worktree", DirWs(directory, workspace)), input, cancellationToken);

    public Task<List<string>> ListWorktreesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<string>>(BuildQuery("/experimental/worktree", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> RemoveWorktreeAsync(WorktreeRemoveInput input, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => DeleteAsync<bool>(BuildQuery("/experimental/worktree", DirWs(directory, workspace)), input, cancellationToken);

    public Task<bool> ResetWorktreeAsync(WorktreeResetInput input, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/experimental/worktree/reset", DirWs(directory, workspace)), input, cancellationToken);

    public Task<List<GlobalSession>> ListSessionsAsync(string? directory = null, string? workspace = null, bool? roots = null, double? start = null, double? cursor = null, string? search = null, int? limit = null, bool? archived = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["roots"] = roots?.ToString();
        query["start"] = start?.ToString();
        query["cursor"] = cursor?.ToString();
        query["search"] = search;
        query["limit"] = limit?.ToString();
        query["archived"] = archived?.ToString();
        return GetAsync<List<GlobalSession>>(BuildQuery("/experimental/session", query), cancellationToken);
    }

    public Task<Dictionary<string, McpResource>> ListResourcesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<Dictionary<string, McpResource>>(BuildQuery("/experimental/resource", DirWs(directory, workspace)), cancellationToken);
}
