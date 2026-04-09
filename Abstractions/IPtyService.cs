using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing pseudo-terminal (PTY) sessions.
/// </summary>
public interface IPtyService
{
    /// <summary>
    /// Get a list of all active pseudo-terminal (PTY) sessions managed by OpenCode.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of PTY sessions.</returns>
    Task<List<Pty>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new pseudo-terminal (PTY) session for running shell commands and processes.
    /// </summary>
    /// <param name="request">The PTY creation request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created PTY session.</returns>
    Task<Pty> CreateAsync(PtyCreateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve detailed information about a specific pseudo-terminal (PTY) session.
    /// </summary>
    /// <param name="ptyId">The PTY session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The PTY session details.</returns>
    Task<Pty> GetAsync(string ptyId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update properties of an existing pseudo-terminal (PTY) session.
    /// </summary>
    /// <param name="ptyId">The PTY session identifier.</param>
    /// <param name="request">The update request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated PTY session.</returns>
    Task<Pty> UpdateAsync(string ptyId, PtyUpdateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove and terminate a specific pseudo-terminal (PTY) session.
    /// </summary>
    /// <param name="ptyId">The PTY session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the PTY session was removed successfully.</returns>
    Task<bool> RemoveAsync(string ptyId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Establish a WebSocket connection to interact with a pseudo-terminal (PTY) session in real-time.
    /// </summary>
    /// <param name="ptyId">The PTY session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if connection was established.</returns>
    Task<bool> ConnectAsync(string ptyId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
