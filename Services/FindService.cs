using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class FindService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IFindService
{
    public Task<List<FindTextMatch>> FindTextAsync(string pattern, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var query = DirWs(directory, workspace);
        query["pattern"] = pattern;
        return GetAsync<List<FindTextMatch>>(BuildQuery("/find", query), cancellationToken);
    }

    public Task<List<string>> FindFilesAsync(string query, string? directory = null, string? workspace = null, string? dirs = null, string? type = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        var queryParams = DirWs(directory, workspace);
        queryParams["query"] = query;
        queryParams["dirs"] = dirs;
        queryParams["type"] = type;
        queryParams["limit"] = limit?.ToString();
        return GetAsync<List<string>>(BuildQuery("/find/file", queryParams), cancellationToken);
    }

    public Task<List<Symbol>> FindSymbolsAsync(string query, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var queryParams = DirWs(directory, workspace);
        queryParams["query"] = query;
        return GetAsync<List<Symbol>>(BuildQuery("/find/symbol", queryParams), cancellationToken);
    }
}
