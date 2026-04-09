using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class PtyService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IPtyService
{
    public Task<List<Pty>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<Pty>>(BuildQuery("/pty", DirWs(directory, workspace)), cancellationToken);

    public Task<Pty> CreateAsync(PtyCreateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<Pty>(BuildQuery("/pty", DirWs(directory, workspace)), request, cancellationToken);

    public Task<Pty> GetAsync(string ptyId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<Pty>(BuildQuery($"/pty/{Uri.EscapeDataString(ptyId)}", DirWs(directory, workspace)), cancellationToken);

    public Task<Pty> UpdateAsync(string ptyId, PtyUpdateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PutAsync<Pty>(BuildQuery($"/pty/{Uri.EscapeDataString(ptyId)}", DirWs(directory, workspace)), request, cancellationToken);

    public Task<bool> RemoveAsync(string ptyId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => DeleteAsync<bool>(BuildQuery($"/pty/{Uri.EscapeDataString(ptyId)}", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> ConnectAsync(string ptyId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<bool>(BuildQuery($"/pty/{Uri.EscapeDataString(ptyId)}/connect", DirWs(directory, workspace)), cancellationToken);
}
