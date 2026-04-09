using System.Text.Json;
using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class ProviderService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IProviderService
{
    public Task<JsonElement> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery("/provider", DirWs(directory, workspace));
        return GetAsync<JsonElement>(url, cancellationToken);
    }

    public Task<Dictionary<string, List<ProviderAuthMethod>>> GetAuthMethodsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery("/provider/auth", DirWs(directory, workspace));
        return GetAsync<Dictionary<string, List<ProviderAuthMethod>>>(url, cancellationToken);
    }

    public Task<ProviderAuthAuthorization> OAuthAuthorizeAsync(string providerId, ProviderOAuthAuthorizeRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/provider/{providerId}/oauth/authorize", DirWs(directory, workspace));
        return PostAsync<ProviderAuthAuthorization>(url, request, cancellationToken);
    }

    public Task<bool> OAuthCallbackAsync(string providerId, ProviderOAuthCallbackRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/provider/{providerId}/oauth/callback", DirWs(directory, workspace));
        return PostAsync<bool>(url, request, cancellationToken);
    }
}
