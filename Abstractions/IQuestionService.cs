using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing question requests from AI assistants.
/// </summary>
public interface IQuestionService
{
    /// <summary>
    /// Get all pending question requests across all sessions.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of pending question requests.</returns>
    Task<List<QuestionRequest>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Provide answers to a question request from the AI assistant.
    /// </summary>
    /// <param name="requestId">The question request identifier.</param>
    /// <param name="request">The reply containing the answers.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the reply was processed successfully.</returns>
    Task<bool> ReplyAsync(string requestId, QuestionReplyRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reject a question request from the AI assistant.
    /// </summary>
    /// <param name="requestId">The question request identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the rejection was processed successfully.</returns>
    Task<bool> RejectAsync(string requestId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
