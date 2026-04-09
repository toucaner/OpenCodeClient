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
