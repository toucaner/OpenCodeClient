using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class ConfigService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IConfigService
{
    public Task<Config> GetAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<Config>(BuildQuery("/config", DirWs(directory, workspace)), cancellationToken);

    public Task<Config> UpdateAsync(Config config, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PatchAsync<Config>(BuildQuery("/config", DirWs(directory, workspace)), config, cancellationToken);

    public Task<ConfigProvidersResponse> GetProvidersAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<ConfigProvidersResponse>(BuildQuery("/config/providers", DirWs(directory, workspace)), cancellationToken);
}
