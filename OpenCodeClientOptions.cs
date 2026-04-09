namespace OpenCodeClient;

/// <summary>
/// Configuration options for the OpenCode client.
/// </summary>
public class OpenCodeClientOptions
{
    /// <summary>
    /// Base URL of the OpenCode server. Defaults to <c>http://localhost:4096</c>.
    /// </summary>
    public string BaseUrl { get; set; } = "http://localhost:4096";

    /// <summary>
    /// Username for HTTP Basic authentication. Defaults to <c>opencode</c>.
    /// Corresponds to the <c>OPENCODE_SERVER_USERNAME</c> environment variable on the server.
    /// </summary>
    public string Username { get; set; } = "opencode";

    /// <summary>
    /// Password for HTTP Basic authentication.
    /// When set, every request includes an <c>Authorization: Basic</c> header.
    /// Corresponds to the <c>OPENCODE_SERVER_PASSWORD</c> environment variable on the server.
    /// </summary>
    public string? Password { get; set; }
}
