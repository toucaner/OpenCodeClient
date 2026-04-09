using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for infrastructure operations including paths, VCS, LSP, formatter status, and event subscriptions.
/// </summary>
public interface IInfraService
{
    /// <summary>
    /// Retrieve the current working directory and related path information for the OpenCode instance.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The path information.</returns>
    Task<PathInfo> GetPathsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve version control system (VCS) information for the current project.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The VCS information including current branch.</returns>
    Task<VcsInfo> GetVcsInfoAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get LSP server status.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of LSP server statuses.</returns>
    Task<List<LspStatus>> GetLspStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get formatter status.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of formatter statuses.</returns>
    Task<List<FormatterStatus>> GetFormatterStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribe to events from the OpenCode system using server-sent events.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An async enumerable stream of events.</returns>
    IAsyncEnumerable<Event> SubscribeToEventsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
