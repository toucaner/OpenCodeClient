using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class AuthService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IAuthService
{
    public Task<bool> SetAsync(string providerId, Auth auth, CancellationToken cancellationToken = default)
        => PutAsync<bool>($"/auth/{Uri.EscapeDataString(providerId)}", auth, cancellationToken);

    public Task<bool> RemoveAsync(string providerId, CancellationToken cancellationToken = default)
        => DeleteAsync<bool>($"/auth/{Uri.EscapeDataString(providerId)}", cancellationToken);
}
