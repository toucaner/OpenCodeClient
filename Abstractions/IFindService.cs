using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for searching text, files, and symbols in the project.
/// </summary>
public interface IFindService
{
    /// <summary>
    /// Search for text patterns across files in the project using ripgrep.
    /// </summary>
    /// <param name="pattern">The text pattern to search for.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of text search matches.</returns>
    Task<List<FindTextMatch>> FindTextAsync(string pattern, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for files or directories by name or pattern in the project directory.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="dirs">Whether to include directories in results.</param>
    /// <param name="type">Filter by type: file or directory.</param>
    /// <param name="limit">Maximum number of results (1-200).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of matching file paths.</returns>
    Task<List<string>> FindFilesAsync(string query, string? directory = null, string? workspace = null, string? dirs = null, string? type = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for workspace symbols like functions, classes, and variables using LSP.
    /// </summary>
    /// <param name="query">The symbol search query.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of matching symbols.</returns>
    Task<List<Symbol>> FindSymbolsAsync(string query, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
