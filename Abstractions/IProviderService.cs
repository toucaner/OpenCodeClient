using System.Text.Json;
using OpenCodeClient.Models;

namespace OpenCodeClient.Abstractions;

/// <summary>
/// Service for managing AI providers and their authentication.
/// </summary>
public interface IProviderService
{
    /// <summary>
    /// Get a list of all available AI providers, including both available and connected ones.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The providers list response as a JSON element due to complex inline schema.</returns>
    Task<JsonElement> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve available authentication methods for all AI providers.
    /// </summary>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A dictionary of provider IDs to their available auth methods.</returns>
    Task<Dictionary<string, List<ProviderAuthMethod>>> GetAuthMethodsAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Initiate OAuth authorization for a specific AI provider to get an authorization URL.
    /// </summary>
    /// <param name="providerId">The provider identifier.</param>
    /// <param name="request">The authorization request with method index.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authorization response with URL and instructions.</returns>
    Task<ProviderAuthAuthorization> OAuthAuthorizeAsync(string providerId, ProviderOAuthAuthorizeRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Handle the OAuth callback from a provider after user authorization.
    /// </summary>
    /// <param name="providerId">The provider identifier.</param>
    /// <param name="request">The callback request with authorization code.</param>
    /// <param name="directory">Optional project directory filter.</param>
    /// <param name="workspace">Optional workspace filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the callback was handled successfully.</returns>
    Task<bool> OAuthCallbackAsync(string providerId, ProviderOAuthCallbackRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default);
}
