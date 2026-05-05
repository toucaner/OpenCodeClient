using System.Threading.Channels;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

public sealed class SseOptions
{
    public int Capacity { get; init; } = 256;
    public TimeSpan InitialInterval { get; init; } = TimeSpan.FromMilliseconds(250);
    public TimeSpan MaxInterval { get; init; } = TimeSpan.FromSeconds(30);
    public double BackoffFactor { get; init; } = 2.0;
    public bool UseJitter { get; init; } = true;
}

public sealed class SseStreamStats
{
    public long EventsIn;
    public long EventsOut;
    public long Dropped;
    public long ParseErrors;
    public long Reconnects;
    public string? LastEventId;
}

public sealed class RawSseEvent
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("event")] public string Event { get; set; } = "";
    [JsonPropertyName("data")] public string Data { get; set; } = "";
}

public abstract class SseSubscription
{
    public abstract SseStreamStats Stats();
    public abstract void Close();
}

public sealed class SseSubscription<T> : SseSubscription, IAsyncEnumerable<T>
{
    private readonly Channel<T> _channel;
    private readonly SseStreamStats _stats;
    private readonly CancellationTokenSource _cts;
    private bool _closed;

    internal SseSubscription(int capacity)
    {
        _channel = Channel.CreateBounded<T>(new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.DropOldest
        });
        _stats = new SseStreamStats();
        _cts = new CancellationTokenSource();
    }

    internal void AddEvent(T? item)
    {
        if (_closed || _cts.IsCancellationRequested)
            return;

        Interlocked.Increment(ref _stats.EventsIn);

        if (item is not null && _channel.Writer.TryWrite(item))
        {
            Interlocked.Increment(ref _stats.EventsOut);
        }
        else
        {
            Interlocked.Increment(ref _stats.Dropped);
        }
    }

    internal void AddParseError()
    {
        Interlocked.Increment(ref _stats.ParseErrors);
        Interlocked.Increment(ref _stats.Dropped);
    }

    internal void SetLastEventId(string? id)
    {
        if (id is not null)
            _stats.LastEventId = id;
    }

    public override SseStreamStats Stats() => new()
    {
        EventsIn = Interlocked.Read(ref _stats.EventsIn),
        EventsOut = Interlocked.Read(ref _stats.EventsOut),
        Dropped = Interlocked.Read(ref _stats.Dropped),
        ParseErrors = Interlocked.Read(ref _stats.ParseErrors),
        Reconnects = Interlocked.Read(ref _stats.Reconnects),
        LastEventId = _stats.LastEventId
    };

    public override void Close()
    {
        if (_closed) return;
        _closed = true;
        _cts.Cancel();
        _channel.Writer.Complete();
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new SseEnumerator(_channel.Reader, cancellationToken);
    }

    private class SseEnumerator(ChannelReader<T> reader, CancellationToken ct) : IAsyncEnumerator<T>
    {
        public T Current { get; private set; } = default!;

        public async ValueTask<bool> MoveNextAsync()
        {
            try
            {
                var result = await reader.ReadAsync(ct).ConfigureAwait(false);
                Current = result;
                return true;
            }
            catch (ChannelClosedException)
            {
                return false;
            }
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

public abstract class SessionEventRouter
{
    public abstract Task<SseSubscription<Event>> SubscribeAsync(string sessionId, CancellationToken cancellationToken = default);
    public abstract SseStreamStats Stats();
    public abstract void Close();
}