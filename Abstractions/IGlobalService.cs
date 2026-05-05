using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for global OpenCode server operations.
/// </summary>
public interface IGlobalService
{
    /// <summary>
    /// Get health information about the OpenCode server.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Health information including server version.</returns>
    Task<HealthResponse> GetHealthAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribe to global events from the OpenCode system using server-sent events.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable stream of global events.</returns>
    IAsyncEnumerable<GlobalEvent> SubscribeToEventsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribe to events for the current directory with reconnection support.
    /// </summary>
    /// <param name="options">SSE subscription options.</param>
    /// <param name="sessionFilter">Optional session ID to filter events.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A subscription to the event stream.</returns>
    Task<SseSubscription<Event>> SubscribeAsync(
        SseOptions options,
        string? sessionFilter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribe to global events (all directories) with reconnection support.
    /// </summary>
    /// <param name="options">SSE subscription options.</param>
    /// <param name="sessionFilter">Optional session ID to filter events.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A subscription to the global event stream.</returns>
    Task<SseSubscription<Event>> SubscribeGlobalAsync(
        SseOptions options,
        string? sessionFilter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribe to raw SSE events for debugging.
    /// </summary>
    /// <param name="options">SSE subscription options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A subscription to raw SSE events.</returns>
    Task<SseSubscription<RawSseEvent>> SubscribeRawAsync(
        SseOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a session event router for per-session event multiplexing.
    /// </summary>
    /// <param name="options">SSE subscription options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A session event router.</returns>
    Task<SessionEventRouter> CreateSessionRouterAsync(
        SseOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve the current global OpenCode configuration settings and preferences.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The current global configuration.</returns>
    Task<Config> GetConfigAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Update global OpenCode configuration settings and preferences.
    /// </summary>
    /// <param name="config">The configuration values to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated global configuration.</returns>
    Task<Config> UpdateConfigAsync(Config config, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clean up and dispose all OpenCode instances, releasing all resources.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if disposal was successful.</returns>
    Task<bool> DisposeAsync(CancellationToken cancellationToken = default);
}
