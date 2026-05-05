using System.Net.Http.Headers;
using System.Text.Json;
using OpenCodeClient.Models;

namespace OpenCodeClient.Events;

public sealed class SseSubscriber
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string? _directory;
    private readonly JsonSerializerOptions _jsonOptions;

    public SseSubscriber(HttpClient httpClient, string baseUrl, string? directory, JsonSerializerOptions jsonOptions)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl.TrimEnd('/');
        _directory = directory;
        _jsonOptions = jsonOptions;
    }

    public Task<SseSubscription<Event>> SubscribeAsync(
        SseOptions options,
        string? sessionFilter,
        CancellationToken cancellationToken = default)
    {
        var subscription = new SseSubscription<Event>(options.Capacity);
        _ = Task.Run(() => StreamEventsAsync(subscription, sessionFilter, options, cancellationToken), cancellationToken);
        return Task.FromResult(subscription);
    }

    public Task<SseSubscription<Event>> SubscribeGlobalAsync(
        SseOptions options,
        string? sessionFilter,
        CancellationToken cancellationToken = default)
    {
        var subscription = new SseSubscription<Event>(options.Capacity);
        _ = Task.Run(() => StreamGlobalEventsAsync(subscription, sessionFilter, options, cancellationToken), cancellationToken);
        return Task.FromResult(subscription);
    }

    public Task<SseSubscription<RawSseEvent>> SubscribeRawAsync(
        SseOptions options,
        CancellationToken cancellationToken = default)
    {
        var subscription = new SseSubscription<RawSseEvent>(options.Capacity);
        _ = Task.Run(() => StreamRawEventsAsync(subscription, options, cancellationToken), cancellationToken);
        return Task.FromResult(subscription);
    }

    private async Task StreamEventsAsync(
        SseSubscription<Event> subscription,
        string? sessionFilter,
        SseOptions options,
        CancellationToken cancellationToken)
    {
        var url = $"{_baseUrl}/event";
        var backoff = new ExponentialBackoff(options.InitialInterval, options.MaxInterval, options.BackoffFactor, options.UseJitter);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await StreamToSubscriptionAsync(subscription, url, sessionFilter, options, backoff, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    await backoff.DelayAsync(cancellationToken);
                }
            }
        }

        subscription.Close();
    }

    private async Task StreamGlobalEventsAsync(
        SseSubscription<Event> subscription,
        string? sessionFilter,
        SseOptions options,
        CancellationToken cancellationToken)
    {
        var url = $"{_baseUrl}/global/event";
        var backoff = new ExponentialBackoff(options.InitialInterval, options.MaxInterval, options.BackoffFactor, options.UseJitter);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await StreamToSubscriptionAsync(subscription, url, sessionFilter, options, backoff, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    await backoff.DelayAsync(cancellationToken);
                }
            }
        }

        subscription.Close();
    }

    private async Task StreamToSubscriptionAsync(
        SseSubscription<Event> subscription,
        string url,
        string? sessionFilter,
        SseOptions options,
        ExponentialBackoff backoff,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));

        if (!string.IsNullOrEmpty(_directory))
        {
            request.Headers.Add("x-opencode-directory", _directory);
        }

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        using var reader = new StreamReader(stream);

        var dataBuffer = new List<string>();
        string? lastEventId = null;

        while (!cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

            if (line is null)
                break;

            if (line.StartsWith("id:", StringComparison.Ordinal))
            {
                lastEventId = line[3..].Trim();
                subscription.SetLastEventId(lastEventId);
            }
            else if (line.StartsWith("data:", StringComparison.Ordinal))
            {
                dataBuffer.Add(line[5..].TrimStart());
            }
            else if (line.Length == 0 && dataBuffer.Count > 0)
            {
                var json = string.Join("\n", dataBuffer);
                dataBuffer.Clear();

                if (string.IsNullOrWhiteSpace(json))
                    continue;

                try
                {
                    var evt = JsonSerializer.Deserialize<Event>(json, _jsonOptions);
                    if (evt is not null)
                    {
                        if (sessionFilter is null || MatchesSession(evt, sessionFilter, json))
                        {
                            subscription.AddEvent(evt);
                        }
                    }
                }
                catch
                {
                    subscription.AddParseError();
                }
            }
        }

        backoff.Reset();
    }

    private async Task StreamRawEventsAsync(
        SseSubscription<RawSseEvent> subscription,
        SseOptions options,
        CancellationToken cancellationToken)
    {
        var url = $"{_baseUrl}/event";
        var backoff = new ExponentialBackoff(options.InitialInterval, options.MaxInterval, options.BackoffFactor, options.UseJitter);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await StreamRawToSubscriptionAsync(subscription, url, options, backoff, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    await backoff.DelayAsync(cancellationToken);
                }
            }
        }

        subscription.Close();
    }

    private async Task StreamRawToSubscriptionAsync(
        SseSubscription<RawSseEvent> subscription,
        string url,
        SseOptions options,
        ExponentialBackoff backoff,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));

        if (!string.IsNullOrEmpty(_directory))
        {
            request.Headers.Add("x-opencode-directory", _directory);
        }

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        using var reader = new StreamReader(stream);

        string? lastEventId = null;
        string? eventType = null;
        var dataBuffer = new List<string>();

        while (!cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

            if (line is null)
                break;

            if (line.StartsWith("id:", StringComparison.Ordinal))
            {
                lastEventId = line[3..].Trim();
                subscription.SetLastEventId(lastEventId);
            }
            else if (line.StartsWith("event:", StringComparison.Ordinal))
            {
                eventType = line[6..].Trim();
            }
            else if (line.StartsWith("data:", StringComparison.Ordinal))
            {
                dataBuffer.Add(line[5..].TrimStart());
            }
            else if (line.Length == 0 && dataBuffer.Count > 0)
            {
                var json = string.Join("\n", dataBuffer);
                dataBuffer.Clear();

                var rawEvent = new RawSseEvent
                {
                    Id = lastEventId ?? "",
                    Event = eventType ?? "message",
                    Data = json
                };

                subscription.AddEvent(rawEvent);
                lastEventId = null;
                eventType = null;
            }
        }

        backoff.Reset();
    }

    private static bool MatchesSession(Event evt, string sessionId, string rawJson)
    {
        var evtSessionId = evt.GetSessionId();
        if (evtSessionId == sessionId)
            return true;

        if (evt is EventMessagePartUpdated { Properties.Part.SessionId: null } && evt is EventMessagePartDelta)
        {
            return rawJson.Contains($"\"sessionID\":\"{sessionId}\"", StringComparison.Ordinal) ||
                   rawJson.Contains($"\"sessionId\":\"{sessionId}\"", StringComparison.Ordinal);
        }

        return evtSessionId is null && rawJson.Contains($"\"sessionID\":\"{sessionId}\"", StringComparison.Ordinal) ||
                                  rawJson.Contains($"\"sessionId\":\"{sessionId}\"", StringComparison.Ordinal);
    }
}

internal sealed class ExponentialBackoff
{
    private readonly TimeSpan _initialInterval;
    private readonly TimeSpan _maxInterval;
    private readonly double _factor;
    private readonly bool _useJitter;
    private TimeSpan _currentInterval;
    private readonly Random _random = new();

    public ExponentialBackoff(TimeSpan initialInterval, TimeSpan maxInterval, double factor, bool useJitter)
    {
        _initialInterval = initialInterval;
        _maxInterval = maxInterval;
        _factor = factor;
        _useJitter = useJitter;
        _currentInterval = initialInterval;
    }

    public void Reset() => _currentInterval = _initialInterval;

    public async Task DelayAsync(CancellationToken cancellationToken)
    {
        var delay = _currentInterval;
        if (_useJitter)
        {
            delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * (0.5 + _random.NextDouble()));
        }

        try
        {
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        _currentInterval = TimeSpan.FromSeconds(Math.Min(_currentInterval.TotalSeconds * _factor, _maxInterval.TotalSeconds));
    }
}