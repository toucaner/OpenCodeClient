using System.Text.Json;
using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for application-level operations including logging, agents, skills, and commands.
/// </summary>
public interface IAppService
{
    /// <summary>
    /// Write a log entry to the server logs with specified level and metadata.
    /// </summary>
    /// <param name="request">The log entry request.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the log entry was written successfully.</returns>
    Task<bool> LogAsync(AppLogRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of all available AI agents in the OpenCode system.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of agents.</returns>
    Task<List<Agent>> ListAgentsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of all available skills in the OpenCode system.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of skills.</returns>
    Task<List<Skill>> ListSkillsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of all available commands in the OpenCode system.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of commands.</returns>
    Task<List<Command>> ListCommandsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clean up and dispose the current OpenCode instance, releasing all resources.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the instance was disposed successfully.</returns>
    Task<bool> DisposeInstanceAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
