using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing OpenCode project-level configuration.
/// </summary>
public interface IConfigService
{
    /// <summary>
    /// Retrieve the current OpenCode configuration settings and preferences.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The current configuration.</returns>
    Task<Config> GetAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update OpenCode configuration settings and preferences.
    /// </summary>
    /// <param name="config">The configuration values to update.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated configuration.</returns>
    Task<Config> UpdateAsync(Config config, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of all configured AI providers and their default models.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The providers configuration with defaults.</returns>
    Task<ConfigProvidersResponse> GetProvidersAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
