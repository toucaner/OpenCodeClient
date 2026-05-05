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

    public async Task<SseSubscription<Event>> SubscribeAsync(
        SseOptions options,
        string? sessionFilter = null,
        CancellationToken cancellationToken = default)
    {
        var subscriber = new SseSubscriber(Http, BaseUrl, null, JsonOptions);
        return await subscriber.SubscribeAsync(options, sessionFilter, cancellationToken);
    }

    public async Task<SseSubscription<Event>> SubscribeGlobalAsync(
        SseOptions options,
        string? sessionFilter = null,
        CancellationToken cancellationToken = default)
    {
        var subscriber = new SseSubscriber(Http, BaseUrl, null, JsonOptions);
        return await subscriber.SubscribeGlobalAsync(options, sessionFilter, cancellationToken);
    }

    public async Task<SseSubscription<RawSseEvent>> SubscribeRawAsync(
        SseOptions options,
        CancellationToken cancellationToken = default)
    {
        var subscriber = new SseSubscriber(Http, BaseUrl, null, JsonOptions);
        return await subscriber.SubscribeRawAsync(options, cancellationToken);
    }

    public async Task<SessionEventRouter> CreateSessionRouterAsync(
        SseOptions options,
        CancellationToken cancellationToken = default)
    {
        var factory = new SessionEventRouterFactory(Http, BaseUrl, null, JsonOptions);
        return await factory.CreateAsync(options, cancellationToken);
    }

    public Task<Config> GetConfigAsync(CancellationToken cancellationToken = default)
        => GetAsync<Config>("/global/config", cancellationToken);

    public Task<Config> UpdateConfigAsync(Config config, CancellationToken cancellationToken = default)
        => PatchAsync<Config>("/global/config", config, cancellationToken);

    public Task<bool> DisposeAsync(CancellationToken cancellationToken = default)
        => PostAsync<bool>("/global/dispose", cancellationToken);
}
