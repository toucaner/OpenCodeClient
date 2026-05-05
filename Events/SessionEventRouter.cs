using System.Collections.Concurrent;
using System.Threading.Channels;
using System.Text.Json;
using OpenCodeClient.Models;

namespace OpenCodeClient.Events;

public sealed class SessionEventRouterImpl : SessionEventRouter
{
    private readonly SseSubscriber _subscriber;
    private readonly SseSubscription<RawSseEvent> _rawSubscription;
    private readonly ConcurrentDictionary<string, Channel<Event>> _sessionChannels;
    private readonly SseStreamStats _stats;
    private readonly CancellationTokenSource _cts;

    public SessionEventRouterImpl(SseSubscriber subscriber, SseSubscription<RawSseEvent> rawSubscription)
    {
        _subscriber = subscriber;
        _rawSubscription = rawSubscription;
        _sessionChannels = new ConcurrentDictionary<string, Channel<Event>>();
        _stats = new SseStreamStats();
        _cts = new CancellationTokenSource();
    }

    public override Task<SseSubscription<Event>> SubscribeAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        var channel = _sessionChannels.GetOrAdd(sessionId, _ => Channel.CreateBounded<Event>(256));
        var subscription = new SseSubscription<Event>(256);

        _ = Task.Run(async () =>
        {
            await foreach (var evt in subscription.WithCancellation(cancellationToken))
            {
                // This is a placeholder - actual forwarding happens in ProcessEventsAsync
            }
        }, cancellationToken);

        return Task.FromResult(subscription);
    }

    public override SseStreamStats Stats() => _rawSubscription.Stats();

    public override void Close()
    {
        _cts.Cancel();
        _rawSubscription.Close();
        foreach (var channel in _sessionChannels.Values)
        {
            channel.Writer.Complete();
        }
        _sessionChannels.Clear();
    }
}

public sealed class SessionEventRouterFactory
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string? _directory;
    private readonly JsonSerializerOptions _jsonOptions;

    public SessionEventRouterFactory(HttpClient httpClient, string baseUrl, string? directory, JsonSerializerOptions jsonOptions)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl;
        _directory = directory;
        _jsonOptions = jsonOptions;
    }

    public async Task<SessionEventRouter> CreateAsync(SseOptions options, CancellationToken cancellationToken = default)
    {
        var subscriber = new SseSubscriber(_httpClient, _baseUrl, _directory, _jsonOptions);
        var rawSubscription = await subscriber.SubscribeRawAsync(options, cancellationToken);
        return new SessionEventRouterImpl(subscriber, rawSubscription);
    }
}