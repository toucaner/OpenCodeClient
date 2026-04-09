using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class PermissionService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IPermissionService
{
    public Task<List<PermissionRequest>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery("/permission", DirWs(directory, workspace));
        return GetAsync<List<PermissionRequest>>(url, cancellationToken);
    }

    public Task<bool> ReplyAsync(string requestId, PermissionReplyRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/permission/{requestId}/reply", DirWs(directory, workspace));
        return PostAsync<bool>(url, request, cancellationToken);
    }
}
