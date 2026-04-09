using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class AppService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IAppService
{
    public Task<bool> LogAsync(AppLogRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/log", DirWs(directory, workspace)), request, cancellationToken);

    public Task<List<Agent>> ListAgentsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<Agent>>(BuildQuery("/agent", DirWs(directory, workspace)), cancellationToken);

    public Task<List<Skill>> ListSkillsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<Skill>>(BuildQuery("/skill", DirWs(directory, workspace)), cancellationToken);

    public Task<List<Command>> ListCommandsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<List<Command>>(BuildQuery("/command", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> DisposeInstanceAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/instance/dispose", DirWs(directory, workspace)), cancellationToken);
}
