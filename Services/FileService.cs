using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class FileService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IFileService
{
    public Task<List<FileNode>> ListAsync(string path, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["path"] = path;
        return GetAsync<List<FileNode>>(BuildQuery("/file", query), cancellationToken);
    }

    public Task<FileContent> ReadAsync(string path, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["path"] = path;
        return GetAsync<FileContent>(BuildQuery("/file/content", query), cancellationToken);
    }

    public Task<List<FileStatus>> GetStatusAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<FileStatus>>(BuildQuery("/file/status", DirWs(directory, workspace)), cancellationToken);
}
