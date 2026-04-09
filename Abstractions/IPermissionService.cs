using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing permission requests from AI assistants.
/// </summary>
public interface IPermissionService
{
    /// <summary>
    /// Get all pending permission requests across all sessions.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of pending permission requests.</returns>
    Task<List<PermissionRequest>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Approve or deny a permission request from the AI assistant.
    /// </summary>
    /// <param name="requestId">The permission request identifier.</param>
    /// <param name="request">The reply containing the decision and optional message.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the reply was processed successfully.</returns>
    Task<bool> ReplyAsync(string requestId, PermissionReplyRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
