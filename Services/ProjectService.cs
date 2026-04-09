using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class ProjectService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IProjectService
{
    public Task<List<Project>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<Project>>(BuildQuery("/project", DirWs(directory, workspace)), cancellationToken);

    public Task<Project> GetCurrentAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<Project>(BuildQuery("/project/current", DirWs(directory, workspace)), cancellationToken);

    public Task<Project> InitGitAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<Project>(BuildQuery("/project/git/init", DirWs(directory, workspace)), cancellationToken);

    public Task<Project> UpdateAsync(string projectId, ProjectUpdateRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PatchAsync<Project>(BuildQuery($"/project/{Uri.EscapeDataString(projectId)}", DirWs(directory, workspace)), request, cancellationToken);
}
