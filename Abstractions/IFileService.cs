using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for file operations within the project.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// List files and directories in a specified path.
    /// </summary>
    /// <param name="path">The path to list files in.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of file nodes.</returns>
    Task<List<FileNode>> ListAsync(string path, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read the content of a specified file.
    /// </summary>
    /// <param name="path">The file path to read.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The file content.</returns>
    Task<FileContent> ReadAsync(string path, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the git status of all files in the project.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of files with their git status.</returns>
    Task<List<FileStatus>> GetStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
