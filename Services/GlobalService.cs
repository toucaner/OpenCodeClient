using OpenCodeClient.Abstractions;
using OpenCodeClient.Events;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class GlobalService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IGlobalService
{
    public Task<HealthResponse> GetHealthAsync(CancellationToken cancellationToken = default)
        => GetAsync<HealthResponse>("/global/health", cancellationToken);

    public IAsyncEnumerable<GlobalEvent> SubscribeToEventsAsync(CancellationToken cancellationToken = default)
        => SseClient.StreamAsync<GlobalEvent>(Http, "/global/event", JsonOptions, cancellationToken);

    public Task<Config> GetConfigAsync(CancellationToken cancellationToken = default)
        => GetAsync<Config>("/global/config", cancellationToken);

    public Task<Config> UpdateConfigAsync(Config config, CancellationToken cancellationToken = default)
        => PatchAsync<Config>("/global/config", config, cancellationToken);

    public Task<bool> DisposeAsync(CancellationToken cancellationToken = default)
        => PostAsync<bool>("/global/dispose", cancellationToken);
}
