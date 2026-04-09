using System.Text.Json;
using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing Model Context Protocol (MCP) servers.
/// </summary>
public interface IMcpService
{
    /// <summary>
    /// Get the status of all Model Context Protocol (MCP) servers.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A dictionary of server names to their statuses.</returns>
    Task<Dictionary<string, McpStatus>> GetStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dynamically add a new Model Context Protocol (MCP) server to the system.
    /// </summary>
    /// <param name="request">The request containing server name and configuration.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A dictionary of server names to their statuses.</returns>
    Task<Dictionary<string, McpStatus>> AddAsync(McpAddRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Start OAuth authentication flow for a Model Context Protocol (MCP) server.
    /// </summary>
    /// <param name="name">The MCP server name.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authorization URL response.</returns>
    Task<McpAuthStartResponse> StartAuthAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove OAuth credentials for an MCP server.
    /// </summary>
    /// <param name="name">The MCP server name.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The removal response.</returns>
    Task<McpAuthRemoveResponse> RemoveAuthAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Complete OAuth authentication for a Model Context Protocol (MCP) server using the authorization code.
    /// </summary>
    /// <param name="name">The MCP server name.</param>
    /// <param name="request">The callback request with authorization code.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The MCP server status after authentication.</returns>
    Task<McpStatus> AuthCallbackAsync(string name, McpAuthCallbackRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Start OAuth flow and wait for callback (opens browser).
    /// </summary>
    /// <param name="name">The MCP server name.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The MCP server status after authentication.</returns>
    Task<McpStatus> AuthenticateAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Connect an MCP server.
    /// </summary>
    /// <param name="name">The MCP server name.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if connected successfully.</returns>
    Task<bool> ConnectAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disconnect an MCP server.
    /// </summary>
    /// <param name="name">The MCP server name.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if disconnected successfully.</returns>
    Task<bool> DisconnectAsync(string name, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
