using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using OpenCodeClient.Converters;

namespace OpenCodeClient.Services;

internal abstract class OpenCodeServiceBase
{
    protected HttpClient Http { get; }
    protected string BaseUrl { get; }
    protected static JsonSerializerOptions JsonOptions { get; } = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new PartConverter(),
            new EventConverter(),
            new PartInputConverter(),
            new ToolStateConverter(),
            new FilePartSourceConverter(),
            new OutputFormatConverter()
        }
    };

    protected OpenCodeServiceBase(HttpClient httpClient)
    {
        Http = httpClient;
        BaseUrl = httpClient.BaseAddress?.ToString().TrimEnd('/') ?? "";
    }

    protected static string BuildQuery(string basePath, Dictionary<string, string?> queryParams)
    {
        var filtered = queryParams.Where(kv => kv.Value is not null).ToList();
        if (filtered.Count == 0) return basePath;

        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var (key, value) in filtered)
            query[key] = value;

        return $"{basePath}?{query}";
    }

    protected static Dictionary<string, string?> DirWs(string? directory, string? workspace)
    {
        return new Dictionary<string, string?>
        {
            ["directory"] = directory,
            ["workspace"] = workspace
        };
    }

    protected async Task<T> GetAsync<T>(string url, CancellationToken ct)
    {
        var response = await Http.GetAsync(url, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false))!;
    }

    protected async Task<T> PostAsync<T>(string url, CancellationToken ct)
    {
        var response = await Http.PostAsync(url, null, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false))!;
    }

    protected async Task<T> PostAsync<T>(string url, object body, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(body, body.GetType(), JsonOptions);
        using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await Http.PostAsync(url, content, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false))!;
    }

    protected async Task PostNoContentAsync(string url, object body, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(body, body.GetType(), JsonOptions);
        using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await Http.PostAsync(url, content, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }

    protected async Task<T> PatchAsync<T>(string url, object body, CancellationToken ct)
    {
        using var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = JsonContent.Create(body, options: JsonOptions)
        };
        var response = await Http.SendAsync(request, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false))!;
    }

    protected async Task<T> PutAsync<T>(string url, object body, CancellationToken ct)
    {
        var response = await Http.PutAsJsonAsync(url, body, JsonOptions, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false))!;
    }

    protected async Task<T> DeleteAsync<T>(string url, CancellationToken ct)
    {
        var response = await Http.DeleteAsync(url, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false))!;
    }

    protected async Task<T> DeleteAsync<T>(string url, object body, CancellationToken ct)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, url)
        {
            Content = JsonContent.Create(body, options: JsonOptions)
        };
        var response = await Http.SendAsync(request, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false))!;
    }
}
