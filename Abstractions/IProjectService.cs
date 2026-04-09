using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing OpenCode projects.
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Get a list of projects that have been opened with OpenCode.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of projects.</returns>
    Task<List<Project>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve the currently active project that OpenCode is working with.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The current project.</returns>
    Task<Project> GetCurrentAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a git repository for the current project and return the refreshed project info.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The project with initialized git repository.</returns>
    Task<Project> InitGitAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update project properties such as name, icon, and commands.
    /// </summary>
    /// <param name="projectId">The project identifier.</param>
    /// <param name="request">The update request with new project properties.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated project.</returns>
    Task<Project> UpdateAsync(string projectId, ProjectUpdateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
