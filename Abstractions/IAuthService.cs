using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing authentication credentials.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Set authentication credentials for a specific provider.
    /// </summary>
    /// <param name="providerId">The provider identifier.</param>
    /// <param name="auth">The authentication credentials to set.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if credentials were set successfully.</returns>
    Task<bool> SetAsync(string providerId, Auth auth, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove authentication credentials for a specific provider.
    /// </summary>
    /// <param name="providerId">The provider identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if credentials were removed successfully.</returns>
    Task<bool> RemoveAsync(string providerId, CancellationToken cancellationToken = default);
}
