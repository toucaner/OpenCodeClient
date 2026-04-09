using OpenCodeClient.Abstractions;
using OpenCodeClient.Events;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class InfraService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IInfraService
{
    public Task<PathInfo> GetPathsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<PathInfo>(BuildQuery("/path", DirWs(directory, workspace)), cancellationToken);

    public Task<VcsInfo> GetVcsInfoAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<VcsInfo>(BuildQuery("/vcs", DirWs(directory, workspace)), cancellationToken);

    public Task<List<LspStatus>> GetLspStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<LspStatus>>(BuildQuery("/lsp", DirWs(directory, workspace)), cancellationToken);

    public Task<List<FormatterStatus>> GetFormatterStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<FormatterStatus>>(BuildQuery("/formatter", DirWs(directory, workspace)), cancellationToken);

    public IAsyncEnumerable<Event> SubscribeToEventsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => SseClient.StreamAsync<Event>(Http, BuildQuery("/event", DirWs(directory, workspace)), JsonOptions, cancellationToken);
}
