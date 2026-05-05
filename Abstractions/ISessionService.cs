using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing OpenCode sessions, messages, and AI interactions.
/// </summary>
public interface ISessionService
{
    /// <summary>
    /// Get a list of all OpenCode sessions, sorted by most recently updated.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="roots">If true, return only root sessions.</param>
    /// <param name="start">Filter sessions updated at or after this timestamp (ms).</param>
    /// <param name="search">Filter sessions by title.</param>
    /// <param name="limit">Maximum number of sessions to return.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of sessions.</returns>
    Task<List<Session>> ListAsync(string? directory = null, string? workspace = null, bool? roots = null, double? start = null, string? search = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new OpenCode session for interacting with AI assistants.
    /// </summary>
    /// <param name="request">The session creation request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created session.</returns>
    Task<Session> CreateAsync(SessionCreateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve the current status of all sessions.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A dictionary of session IDs to their statuses.</returns>
    Task<Dictionary<string, SessionStatus>> GetStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve detailed information about a specific OpenCode session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session details.</returns>
    Task<Session> GetAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a session and permanently remove all associated data.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the session was deleted successfully.</returns>
    Task<bool> DeleteAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update properties of an existing session, such as title or other metadata.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The update request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated session.</returns>
    Task<Session> UpdateAsync(string sessionId, SessionUpdateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve all child sessions that were forked from the specified parent session.
    /// </summary>
    /// <param name="sessionId">The parent session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of child sessions.</returns>
    Task<List<Session>> GetChildrenAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve the todo list associated with a specific session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of todo items.</returns>
    Task<List<Todo>> GetTodosAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyze the current application and create an AGENTS.md file with project-specific agent configurations.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The initialization request with model and provider info.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if initialization was successful.</returns>
    Task<bool> InitAsync(string sessionId, SessionInitRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new session by forking an existing session at a specific message point.
    /// </summary>
    /// <param name="sessionId">The session identifier to fork.</param>
    /// <param name="request">The fork request with optional message point.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The forked session.</returns>
    Task<Session> ForkAsync(string sessionId, SessionForkRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abort an active session and stop any ongoing AI processing or command execution.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the session was aborted successfully.</returns>
    Task<bool> AbortAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a shareable link for a session, allowing others to view the conversation.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session with share information.</returns>
    Task<Session> ShareAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove the shareable link for a session, making it private again.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session without share information.</returns>
    Task<Session> UnshareAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the file changes (diff) that resulted from a specific user message in the session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="messageId">Optional message identifier to get diff for.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of file diffs.</returns>
    Task<List<FileDiff>> GetDiffAsync(string sessionId, string? messageId = null, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate a concise summary of the session using AI compaction.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The summarize request with model info.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if summarization was successful.</returns>
    Task<bool> SummarizeAsync(string sessionId, SessionSummarizeRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve all messages in a session, including user prompts and AI responses.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="limit">Maximum number of messages to return.</param>
    /// <param name="before">Cursor for pagination - return messages before this ID.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of messages with their parts.</returns>
    Task<List<MessageWithParts>> GetMessagesAsync(string sessionId, int? limit = null, string? before = null, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create and send a new message to a session, streaming the AI response.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The prompt request with message parts.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The assistant message with response parts.</returns>
    Task<AssistantMessageWithParts> PromptAsync(string sessionId, SessionPromptRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create and send a new message to a session asynchronously, returning immediately.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The prompt request with message parts.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PromptAsyncFire(string sessionId, SessionPromptRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve a specific message from a session by its message ID.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="messageId">The message identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The message with its parts.</returns>
    Task<MessageWithParts> GetMessageAsync(string sessionId, string messageId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Permanently delete a specific message and all of its parts from a session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="messageId">The message identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the message was deleted successfully.</returns>
    Task<bool> DeleteMessageAsync(string sessionId, string messageId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a part from a message.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="messageId">The message identifier.</param>
    /// <param name="partId">The part identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the part was deleted successfully.</returns>
    Task<bool> DeletePartAsync(string sessionId, string messageId, string partId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a part in a message.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="messageId">The message identifier.</param>
    /// <param name="partId">The part identifier.</param>
    /// <param name="part">The updated part data.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated part.</returns>
    Task<Part> UpdatePartAsync(string sessionId, string messageId, string partId, Part part, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send a new command to a session for execution by the AI assistant.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The command request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The assistant message with response parts.</returns>
    Task<AssistantMessageWithParts> SendCommandAsync(string sessionId, SessionCommandRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute a shell command within the session context and return the AI's response.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The shell command request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The assistant message response.</returns>
    Task<AssistantMessage> RunShellAsync(string sessionId, SessionShellRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revert a specific message in a session, undoing its effects and restoring the previous state.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="request">The revert request with message ID.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session after revert.</returns>
    Task<Session> RevertAsync(string sessionId, SessionRevertRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restore all previously reverted messages in a session.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session after unrevert.</returns>
    Task<Session> UnrevertAsync(string sessionId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Approve or deny a permission request from the AI assistant. This method is deprecated.
    /// </summary>
    /// <param name="sessionId">The session identifier.</param>
    /// <param name="permissionId">The permission request identifier.</param>
    /// <param name="request">The permission response.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the response was processed successfully.</returns>
    [Obsolete("Use IPermissionService.ReplyAsync instead.")]
    Task<bool> RespondToPermissionAsync(string sessionId, string permissionId, PermissionRespondRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribe to events for a specific session with reconnection support.
    /// </summary>
    /// <param name="sessionId">The session identifier to filter events.</param>
    /// <param name="options">SSE subscription options.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A subscription to session events.</returns>
    Task<SseSubscription<Event>> SubscribeAsync(
        string sessionId,
        SseOptions options,
        string? directory = null,
        string? workspace = null,
        CancellationToken cancellationToken = default);
}
