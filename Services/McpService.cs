using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class McpService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IMcpService
{
    public Task<Dictionary<string, McpStatus>> GetStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<Dictionary<string, McpStatus>>(BuildQuery("/mcp", DirWs(directory, workspace)), cancellationToken);

    public Task<Dictionary<string, McpStatus>> AddAsync(McpAddRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<Dictionary<string, McpStatus>>(BuildQuery("/mcp", DirWs(directory, workspace)), request, cancellationToken);

    public Task<McpAuthStartResponse> StartAuthAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<McpAuthStartResponse>(BuildQuery($"/mcp/{Uri.EscapeDataString(name)}/auth", DirWs(directory, workspace)), cancellationToken);

    public Task<McpAuthRemoveResponse> RemoveAuthAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => DeleteAsync<McpAuthRemoveResponse>(BuildQuery($"/mcp/{Uri.EscapeDataString(name)}/auth", DirWs(directory, workspace)), cancellationToken);

    public Task<McpStatus> AuthCallbackAsync(string name, McpAuthCallbackRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<McpStatus>(BuildQuery($"/mcp/{Uri.EscapeDataString(name)}/auth/callback", DirWs(directory, workspace)), request, cancellationToken);

    public Task<McpStatus> AuthenticateAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<McpStatus>(BuildQuery($"/mcp/{Uri.EscapeDataString(name)}/auth/authenticate", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> ConnectAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery($"/mcp/{Uri.EscapeDataString(name)}/connect", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> DisconnectAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery($"/mcp/{Uri.EscapeDataString(name)}/disconnect", DirWs(directory, workspace)), cancellationToken);
}
