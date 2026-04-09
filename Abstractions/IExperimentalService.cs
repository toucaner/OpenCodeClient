using System.Text.Json;
using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for experimental OpenCode features including tools, workspaces, and worktrees.
/// </summary>
public interface IExperimentalService
{
    /// <summary>
    /// Get a list of all available tool IDs, including both built-in tools and dynamically registered tools.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of tool IDs.</returns>
    Task<List<string>> GetToolIdsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of available tools with their JSON schema parameters for a specific provider and model combination.
    /// </summary>
    /// <param name="provider">The provider identifier.</param>
    /// <param name="model">The model identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of tools with their schemas.</returns>
    Task<List<ToolListItem>> ListToolsAsync(string provider, string model, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a workspace for the current project.
    /// </summary>
    /// <param name="request">The workspace creation request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created workspace.</returns>
    Task<Workspace> CreateWorkspaceAsync(WorkspaceCreateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// List all workspaces.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of workspaces.</returns>
    Task<List<Workspace>> ListWorkspacesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove an existing workspace.
    /// </summary>
    /// <param name="id">The workspace identifier.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The removed workspace.</returns>
    Task<Workspace> RemoveWorkspaceAsync(string id, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new git worktree for the current project and run any configured startup scripts.
    /// </summary>
    /// <param name="input">The worktree creation input.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created worktree.</returns>
    Task<Worktree> CreateWorktreeAsync(WorktreeCreateInput input, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// List all sandbox worktrees for the current project.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of worktree directory paths.</returns>
    Task<List<string>> ListWorktreesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove a git worktree and delete its branch.
    /// </summary>
    /// <param name="input">The worktree removal input.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the worktree was removed successfully.</returns>
    Task<bool> RemoveWorktreeAsync(WorktreeRemoveInput input, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reset a worktree branch to the primary default branch.
    /// </summary>
    /// <param name="input">The worktree reset input.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the worktree was reset successfully.</returns>
    Task<bool> ResetWorktreeAsync(WorktreeResetInput input, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of all OpenCode sessions across projects, sorted by most recently updated.
    /// </summary>
    /// <param name="directory">Optional filter by project directory.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="roots">If true, return only root sessions.</param>
    /// <param name="start">Filter sessions updated at or after this timestamp (ms).</param>
    /// <param name="cursor">Return sessions updated before this timestamp.</param>
    /// <param name="search">Filter sessions by title.</param>
    /// <param name="limit">Maximum number of sessions to return.</param>
    /// <param name="archived">If true, include archived sessions.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of global sessions.</returns>
    Task<List<GlobalSession>> ListSessionsAsync(string? directory = null, string? workspace = null, bool? roots = null, double? start = null, double? cursor = null, string? search = null, int? limit = null, bool? archived = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all available MCP resources from connected servers.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A dictionary of resource names to their details.</returns>
    Task<Dictionary<string, McpResource>> ListResourcesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
