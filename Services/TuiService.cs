using System.Text.Json;
using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class TuiService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), ITuiService
{
    public Task<bool> AppendPromptAsync(TuiAppendPromptRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/append-prompt", DirWs(directory, workspace)), request, cancellationToken);

    public Task<bool> OpenHelpAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/open-help", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> OpenSessionsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/open-sessions", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> OpenThemesAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/open-themes", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> OpenModelsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/open-models", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> SubmitPromptAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/submit-prompt", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> ClearPromptAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/clear-prompt", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> ExecuteCommandAsync(TuiExecuteCommandRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/execute-command", DirWs(directory, workspace)), request, cancellationToken);

    public Task<bool> ShowToastAsync(TuiShowToastRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/show-toast", DirWs(directory, workspace)), request, cancellationToken);

    public Task<bool> PublishAsync(JsonElement eventData, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/publish", DirWs(directory, workspace)), eventData, cancellationToken);

    public Task<bool> SelectSessionAsync(TuiSelectSessionRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/select-session", DirWs(directory, workspace)), request, cancellationToken);

    public Task<TuiControlNextResponse> GetControlNextAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => GetAsync<TuiControlNextResponse>(BuildQuery("/tui/control/next", DirWs(directory, workspace)), cancellationToken);

    public Task<bool> SubmitControlResponseAsync(JsonElement body, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
        => PostAsync<bool>(BuildQuery("/tui/control/response", DirWs(directory, workspace)), body, cancellationToken);
}
