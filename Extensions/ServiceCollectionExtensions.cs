using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenCodeClient.Abstractions;
using OpenCodeClient.Services;

namespace OpenCodeClient.Extensions;

/// <summary>
/// Extension methods for registering OpenCode client services with the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string HttpClientName = "OpenCode";

    /// <summary>
    /// Registers all OpenCode client services and configures the underlying <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">An action to configure <see cref="OpenCodeClientOptions"/>.</param>
    /// <returns>The <see cref="IHttpClientBuilder"/> for further HTTP client configuration.</returns>
    public static IHttpClientBuilder AddOpenCodeClient(
        this IServiceCollection services,
        Action<OpenCodeClientOptions> configure)
    {
        services.Configure(configure);

        var builder = services.AddHttpClient(HttpClientName, (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<OpenCodeClientOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/'));
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

            if (!string.IsNullOrEmpty(options.Password))
            {
                var credentials = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}"));
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", credentials);
            }
        });

        services.AddScoped<IGlobalService>(sp => new GlobalService(CreateClient(sp)));
        services.AddScoped<IAuthService>(sp => new AuthService(CreateClient(sp)));
        services.AddScoped<IProjectService>(sp => new ProjectService(CreateClient(sp)));
        services.AddScoped<IPtyService>(sp => new PtyService(CreateClient(sp)));
        services.AddScoped<IConfigService>(sp => new ConfigService(CreateClient(sp)));
        services.AddScoped<ISessionService>(sp => new SessionService(CreateClient(sp)));
        services.AddScoped<IPermissionService>(sp => new PermissionService(CreateClient(sp)));
        services.AddScoped<IQuestionService>(sp => new QuestionService(CreateClient(sp)));
        services.AddScoped<IProviderService>(sp => new ProviderService(CreateClient(sp)));
        services.AddScoped<IFindService>(sp => new FindService(CreateClient(sp)));
        services.AddScoped<IFileService>(sp => new FileService(CreateClient(sp)));
        services.AddScoped<IMcpService>(sp => new McpService(CreateClient(sp)));
        services.AddScoped<ITuiService>(sp => new TuiService(CreateClient(sp)));
        services.AddScoped<IExperimentalService>(sp => new ExperimentalService(CreateClient(sp)));
        services.AddScoped<IAppService>(sp => new AppService(CreateClient(sp)));
        services.AddScoped<IInfraService>(sp => new InfraService(CreateClient(sp)));

        return builder;
    }

    /// <summary>
    /// Registers all OpenCode client services with the default base URL (<c>http://localhost:4096</c>).
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IHttpClientBuilder"/> for further HTTP client configuration.</returns>
    public static IHttpClientBuilder AddOpenCodeClient(this IServiceCollection services)
        => services.AddOpenCodeClient(_ => { });

    private static HttpClient CreateClient(IServiceProvider sp)
        => sp.GetRequiredService<IHttpClientFactory>().CreateClient(HttpClientName);
}
