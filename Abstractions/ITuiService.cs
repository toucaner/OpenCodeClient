using System.Text.Json;
using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for interacting with the OpenCode terminal user interface (TUI).
/// </summary>
public interface ITuiService
{
    /// <summary>
    /// Append text to the TUI prompt input.
    /// </summary>
    /// <param name="request">The request containing the text to append.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the text was appended successfully.</returns>
    Task<bool> AppendPromptAsync(TuiAppendPromptRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Open the help dialog in the TUI.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the dialog was opened successfully.</returns>
    Task<bool> OpenHelpAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Open the sessions dialog in the TUI.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the dialog was opened successfully.</returns>
    Task<bool> OpenSessionsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Open the themes dialog in the TUI.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the dialog was opened successfully.</returns>
    Task<bool> OpenThemesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Open the models dialog in the TUI.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the dialog was opened successfully.</returns>
    Task<bool> OpenModelsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Submit the current TUI prompt.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the prompt was submitted successfully.</returns>
    Task<bool> SubmitPromptAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clear the current TUI prompt.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the prompt was cleared successfully.</returns>
    Task<bool> ClearPromptAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute a command in the TUI.
    /// </summary>
    /// <param name="request">The request containing the command to execute.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the command was executed successfully.</returns>
    Task<bool> ExecuteCommandAsync(TuiExecuteCommandRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Show a toast notification in the TUI.
    /// </summary>
    /// <param name="request">The toast notification request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the toast was shown successfully.</returns>
    Task<bool> ShowToastAsync(TuiShowToastRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publish a TUI event.
    /// </summary>
    /// <param name="eventData">The event data to publish.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the event was published successfully.</returns>
    Task<bool> PublishAsync(JsonElement eventData, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Select a session in the TUI.
    /// </summary>
    /// <param name="request">The request containing the session ID to select.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the session was selected successfully.</returns>
    Task<bool> SelectSessionAsync(TuiSelectSessionRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the next TUI control request.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The next control request with path and body.</returns>
    Task<TuiControlNextResponse> GetControlNextAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Submit a TUI control response.
    /// </summary>
    /// <param name="body">The response body as JSON.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the response was submitted successfully.</returns>
    Task<bool> SubmitControlResponseAsync(JsonElement body, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
